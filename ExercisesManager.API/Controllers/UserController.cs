using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExercisesManager.API.InputModels;
using ExercisesManager.API.Logging;
using ExercisesManager.API.Services.Interfaces;
using ExercisesManager.Identity.Config;
using ExercisesManager.Identity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ExercisesManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _usersService;
        private readonly IdentityConfig _identityConfig;

        public UserController(
            IUserService usersService,
            IdentityConfig identityConfig)
        {
            _usersService = usersService;
            _identityConfig = identityConfig;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] LoginUserInputModel loginViewModel)
        {
            var applicationUser = await _usersService.IsUserAuthenticated(loginViewModel);

            if (applicationUser != null)
                return GenerateToken(applicationUser);

            return BadRequest(new ErrorResponseDTO
            {
                Message = "Username or Password is invalid",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        private ActionResult GenerateToken(ApplicationUser applicationUser)
        {
            var userRoles = applicationUser.UserRoles.Select(x => x.Role.Name);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, applicationUser.UserName)
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfig.IdentitySecret));

            var token = new JwtSecurityToken(
                issuer: _identityConfig.IdentityIssuer,
                audience: _identityConfig.IdentityIssuer,
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                userRoles = userRoles.ToArray(),
                fullname = $"{applicationUser.FirstName} {applicationUser.LastName}",
                email = applicationUser.Email
            });
        }

    }
}