using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestMySql.DTO.request;
using System.Threading.Tasks;
using TestMySql.Repositories.Interface;
using TestMySql.DTO.response;

namespace TestMySql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            //_roleManager = roleManager;
            _tokenRepository = tokenRepository;
        }

        // POST: api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username
            };

            var identityResult = await _userManager.CreateAsync(user, registerRequest.Password);
            if (identityResult.Succeeded)
            {
                // Add roles to user
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(user, registerRequest.Roles);
                }

                return Ok("User registered successfully.");
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

    // POST: api/Auth/Login
    [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Username);
            if (user != null)
            {
                var checkResultPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
                if (checkResultPassword)
                {
                    //Get Roles for this user
                    var roles = await _userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        //Create Token
                        var jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponse
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest(ModelState);
        }
    }
}
