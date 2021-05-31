using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Workshop.API.DTOs;
using Workshop.API.Entities;
using Workshop.API.Interfaces;

namespace Workshop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(ITokenService tokenService, UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(User user)
        {
            //TODO: Add register DTO
            var result = await _userManager.CreateAsync(user, "Pa$$w0rd");

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if(!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(User login)
        {
            //TODO: Add login DTO
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == login.UserName.ToUpper());

            if(user == null)
                return Unauthorized("Wrong username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, "Pa$$w0rd", false);

            if(!result.Succeeded)
                return Unauthorized();

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpGet]
        public IActionResult Test()
        {
            return View("<h3>Hello</h3>");
        }
    }
}