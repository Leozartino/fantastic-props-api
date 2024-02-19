using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FantasticProps.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AuthController : ControllerBase
    {

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IJwtSettingsHelper _jwtHelper;

        public AuthController(
            SignInManager<IdentityUser> 
            signInManager, UserManager<IdentityUser> userManager, 
            IOptions<JwtSettings> jwtSettings,
            IJwtSettingsHelper jwtSettingsHelper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _jwtHelper = jwtSettingsHelper;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterUser registerUser)
        {
            IdentityUser user = new()
            {
               UserName = registerUser.Email,
               Email = registerUser.Email,
               //In a real scenario, you should verify the email to confirm the user first, before proceed to the
               //register user
               EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if(result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(_jwtHelper.GenerateJWT(_jwtSettings));
            }

            return Problem("Error while trying to register the user");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUser loginUser)
        {

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return Ok(_jwtHelper.GenerateJWT(_jwtSettings));
            }

            return Problem("User and/or password incorrect!");
        }
    }
}
