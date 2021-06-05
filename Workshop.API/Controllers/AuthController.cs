using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthController(ITokenService tokenService, UserManager<User> userManager,
            SignInManager<User> signInManager, IMapper mapper)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto register)
        {
            if (await UserExists(register.UserName))
                return BadRequest("Nazwa użytkownika jest zajęta.");

            var user = _mapper.Map<User>(register);

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
                return BadRequest(roleResult.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto login)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.NormalizedUserName == login.UserName.ToUpper());

            if (user == null)
                return Unauthorized("Użytkownik nie istnieje.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
                return Unauthorized();

            return new UserDto
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExists(string username) =>
            await _userManager.Users.AnyAsync(u => u.NormalizedUserName == username.ToUpper());
    }
}