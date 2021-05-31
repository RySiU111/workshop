using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.API.Entities;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            //TODO: Add DTOs
            var result = await _userManager.CreateAsync(user, "Pa$$w0rd");

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(User login)
        {
            //TODO: Add Token and DTOs
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == login.UserName.ToUpper());

            if(user == null)
                return Unauthorized();

            var result = await _signInManager.CheckPasswordSignInAsync(user, "Pa$$w0rd", false);

            if(!result.Succeeded)
                return Unauthorized();

            return user;
        }
    }
}