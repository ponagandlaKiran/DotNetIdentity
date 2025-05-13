using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User.Management.API.Models.Authentication;
using User.Management.API.Models.Authentication.SignUp;

namespace User.Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("RegisterNewuser")]
        public async Task<IActionResult> RegisterUser(RegisterUser registerUser)
        {
            
            if (registerUser == null)
            {
                return BadRequest(new Response { Status = "Error", Message = "Invalid user data." });
            }

            // Check if user already exists
            var userExists = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new Response { Status = "Error", Message = "User already exists." });
            }


            if(await _roleManager.RoleExistsAsync(registerUser.UserRole))
            {
                // Create new user
                var user = new IdentityUser
                {
                    Email = registerUser.Email,
                    UserName = registerUser.UserName,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = await _userManager.CreateAsync(user, registerUser.Password);

                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response
                    {
                        Status = "Error",
                        Message = "User creation failed: " + string.Join("; ", result.Errors.Select(e => e.Description))
                    });
                }

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, registerUser.UserRole);

                }

            }
        
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
