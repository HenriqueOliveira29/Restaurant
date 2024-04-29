using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.BLL.Services;
using Restaurant.Infraestructure.Helpers;
using Restaurant.Infraestructure.Model.Auth;

namespace Restaurant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public async Task<MessagingHelper<AuthDTO>> Login([FromBody] LoginDTO login)
        {
            return await _authService.Login(login);
        }
    }
}
