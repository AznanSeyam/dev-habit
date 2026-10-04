using dev_habit.Models;
using dev_habit.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dev_habit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IJwtRepository _jwtRepository;

        public AuthController(UserManager<IdentityUser> userManager, IJwtRepository jwtRepository)
        {
            _userManager = userManager;
            _jwtRepository = jwtRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] CreateAuthDTO createAuthDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = createAuthDTO.Username,
                Email = createAuthDTO.Username
            };

            var identityResult = await _userManager.CreateAsync(identityUser, createAuthDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this User
                if (createAuthDTO.Roles != null && createAuthDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, createAuthDTO.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }
            return BadRequest("went  wrong!");
        }

        // POST: /api/Auth/Login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CreateLoginDTO createLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(createLoginDTO.Username);

            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, createLoginDTO.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create Token

                        var jwtToken = _jwtRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            Token = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password incorrect");
        }
































    }
}
