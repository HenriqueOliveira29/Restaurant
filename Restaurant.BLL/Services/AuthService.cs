using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Infraestructure.Entities;
using Restaurant.Infraestructure.Helpers;
using Restaurant.Infraestructure.Model.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.BLL.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private IOptions<IdentityOptions> _optionsAccessor;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher, IOptions<IdentityOptions> optionsAccessor, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _optionsAccessor = optionsAccessor;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<MessagingHelper<AuthDTO>> Login(LoginDTO loginDTO)
        {
            MessagingHelper<AuthDTO> response = new MessagingHelper<AuthDTO>();

            try
            {
                //validate DTO
                LoginDTOValidator validator = new();
                var validate = await validator.ValidateAsync(loginDTO);
                if (!validate.IsValid)
                {
                    response.Success = false;
                    response.Message = validate.Errors.FirstOrDefault().ErrorMessage;
                    return response;
                }

                ApplicationUser? user = await AuthenticateUser(loginDTO);

                if (user != null)
                {
                    if (!user.Active)
                    {
                        response.Success = false;
                        response.Message = "This user is not active";
                        return response;
                    }

                    var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
                    if (userRoles == null || userRoles.Count == 0)
                    {
                        response.Success = false;
                        response.Message = "User with invalid configuration";
                        return response;
                    }

                    var token = await GenerateJSONToken(user, userRoles);

                    AuthDTO responseObj = new AuthDTO()
                    {
                        Token = token,
                        Id = user.Id,
                        Username = user.Name,
                        Roles = userRoles.ToArray(),
                        PreferedView = user.PreferedView
                    };

                    SaveToken(responseObj);
                    SaveCookie();
                    response.Success = true;
                    response.Obj = responseObj;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Incorect data";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.InnerException.GetBaseException().Message;
            }
            return response;
        }

        private async Task<string> GenerateJSONToken(ApplicationUser userInfo, List<string> userRoles)
        {
            var user = await _userManager.FindByIdAsync(userInfo.Id);
            if (user == null)
            {
                return "";
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? ""));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Vemos as claims que já tem
            var claims = GetTokenClaims(user).Union(userClaims).ToList();

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims: claims,
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser?> AuthenticateUser(LoginDTO loginDTO)
        {
            ApplicationUser? user = null;
            try
            {
                var userFound = await _userManager.FindByEmailAsync(loginDTO.Email);
                if (userFound != null)
                {
                    if (userFound.Active == false) return null;

                    if (userFound.LockoutEnd != null)
                    {
                        if (userFound.LockoutEnd > DateTime.UtcNow) return null;

                        //Se o utilizador tiver tido um lockout, mas agora já passou removemos o lockout e o accessFailed
                        userFound.LockoutEnd = null;
                        userFound.AccessFailedCount = 0;
                    }

                    if (_passwordHasher.VerifyHashedPassword(userFound, userFound.PasswordHash, loginDTO.Password) == PasswordVerificationResult.Success)
                    {
                        user = userFound;

                        user.AccessFailedCount = 0;
                        user.LastLockoutDuration = 0;
                        user.LockoutEnd = null;

                        await _userManager.UpdateAsync(user);
                    }
                    else
                    {
                        await UserAccessFailed(userFound);
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteLogs
            }
            return user;
        }

        private async Task UserAccessFailed(ApplicationUser user)
        {
            user.AccessFailedCount++;

            //Se passar o limite de tentativas
            if (user.AccessFailedCount >= _optionsAccessor.Value.Lockout.MaxFailedAccessAttempts)
            {
                if (user.LastLockoutDuration == 0)
                {
                    user.LastLockoutDuration = 5;
                }
                else
                {
                    user.LastLockoutDuration *= 2;
                }

                user.LastLockoutDuration = user.LastLockoutDuration >= 10000 ? 10000 : user.LastLockoutDuration;

                user.LockoutEnd = DateTime.UtcNow.AddMinutes(user.LastLockoutDuration);
            }

            await _userManager.UpdateAsync(user);
        }

        private static IEnumerable<Claim> GetTokenClaims(IdentityUser user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };
        }

        public virtual MessagingHelper SaveToken(AuthDTO user)
        {
            MessagingHelper response = new();
            try
            {
                _httpContextAccessor.HttpContext.Session.SetString("token", user.Token);
                _httpContextAccessor.HttpContext.Session.SetString("username", user.Username);
                _httpContextAccessor.HttpContext.Session.SetString("id", user.Id);
                _httpContextAccessor.HttpContext.Session.SetString("roles", string.Join(",", user.Roles));
                _httpContextAccessor.HttpContext.Session.SetString("preferedView", user.PreferedView);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um problema ao fazer login, por favor tente outra vez.";
            }

            return response;
        }

        public virtual MessagingHelper SaveCookie()
        {
            MessagingHelper response = new();
            try
            {
                var cookie = _httpContextAccessor.HttpContext.Request.Cookies.Where(t => t.Key == "tabletId").FirstOrDefault();
                if (cookie.Value == null)
                {
                    var bytes = new byte[4];
                    var rng = RandomNumberGenerator.Create();
                    rng.GetBytes(bytes);
                    uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
                    var token = String.Format("{0:D10}", random);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append("tabletId", token, new CookieOptions()
                    {
                        Expires = DateTime.Now.AddYears(100),
                        Secure = true,
                        SameSite = SameSiteMode.None,
                    });
                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Ocorreu um problema ao fazer login, por favor tente outra vez.";
            }
            return response;
        }
    }
}
