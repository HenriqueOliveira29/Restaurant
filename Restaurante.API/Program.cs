using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.DAL;
using Restaurant.Infraestructure.Entities;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
  .AddEntityFrameworkStores<ApplicationDBContext>()
  .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
       {
           ClockSkew = TimeSpan.Zero,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),
           ValidateLifetime = false,
           ValidateAudience = true,
           ValidAudience = builder.Configuration["Jwt:Issuer"],
           ValidateIssuer = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidateIssuerSigningKey = true
       };
       options.Events = new JwtBearerEvents
       {
           OnAuthenticationFailed = ctx =>
           {
               ctx.Response.StatusCode = 401;
               return Task.FromResult<object>(null);
           },
           OnTokenValidated = context =>
           {
               var _userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

               //Obtemos o userName do user
               var userId = context.Principal?.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

               //Se nao encontrarmos esse campo
               if (userId is null)
               {
                   context.Response.StatusCode = 401;
                   context.Fail("Invalid Data");
                   return Task.CompletedTask;
               }

               //Vamos obter o user pelo email
               var user = _userManager.Users
                   .Include(u => u.UserRoles)
                   .ThenInclude(ur => ur.Role)
                   .Single(o => o.Id == userId);

               //User nao existe
               if (user is null)
               {
                   context.Response.StatusCode = 401;
                   context.Fail("Invalid Data");
                   return Task.CompletedTask;
               }

               //Utilizador nao esta ativo
               if (user.Active == false)
               {
                   context.Response.StatusCode = 401;
                   context.Fail("Invalid Data");
                   return Task.CompletedTask;
               }

               //Utilizador esta bloqueado
               if (user.LockoutEnd > DateTime.Now)
               {
                   context.Response.StatusCode = 401;
                   context.Fail("Invalid Data");
                   return Task.CompletedTask;
               }

               //Obtemos todos os roles do JWT
               var jwtUserRoles = context.Principal.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
               if (jwtUserRoles.Count == 0)
               {
                   context.Response.StatusCode = 401;
                   context.Fail("Invalid Data");
                   return Task.CompletedTask;
               }

               //Obtemos todos os roles que o user tem na BD
               var userRoles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

               if (userRoles.Count == 0)
               {
                   context.Response.StatusCode = 401;
                   context.Fail("Invalid Data");
                   return Task.CompletedTask;
               }

               foreach (var jwtUserRole in jwtUserRoles)
               {
                   //Se o JWT tiver um role que a BD nao tem saimos
                   var doesUserHaveRole = userRoles.Contains(jwtUserRole.Value);
                   if (doesUserHaveRole == false)
                   {
                       context.Response.StatusCode = 401;
                       context.Fail("Invalid Data");
                       return Task.CompletedTask;
                   }
               }

               return Task.CompletedTask;
           }
       };

   });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
