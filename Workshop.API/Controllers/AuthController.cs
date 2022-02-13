using System;
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
        private readonly RoleManager<Role> _roleManager;

        public AuthController(ITokenService tokenService, UserManager<User> userManager,
            SignInManager<User> signInManager, IMapper mapper, IUnitOfWork unitOfWork, RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
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

        [HttpPut]
        [Route("changePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UserChangePasswordDto userDto)
        {
            if (userDto == null)
                return BadRequest();

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);

            if (user == null)
                return NotFound(userDto);

            var result = await _userManager.ChangePasswordAsync(user, userDto.CurrentPassword, userDto.NewPassword);

            if (result.Succeeded)
                return Ok();

            return StatusCode(500, result.Errors);
        }

        [HttpGet]
        [Route("users")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await _userManager.Users
                .Where(u => u.IsActive)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.User)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
            var usersToReturn = _mapper.Map<UserDetailsDto[]>(users);

            return Ok(usersToReturn);
        }

        [HttpDelete]
        [Route("user")]
        public async Task<ActionResult> RemoveUser([FromQuery] int userId)
        {
            if (userId <= 0)
                return BadRequest();

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            user.IsActive = false;
            var lockResult = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded && lockResult.Succeeded)
                return Ok();

            return BadRequest(new[] { result.Errors, lockResult.Errors });
        }

        [HttpPut]
        [Route("user")]
        public async Task<ActionResult> EditUser([FromBody] UserEditDto userDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);

            if (user == null)
                return NotFound();

            user.UserName = userDto.UserName;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Email = userDto.Email;


            if (user.IsActive == true && userDto.IsActive == false)
            {
                user.IsActive = userDto.IsActive;
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);
            }
            else if (user.IsActive == false && userDto.IsActive == true)
            {
                user.IsActive = userDto.IsActive;
                await _userManager.SetLockoutEndDateAsync(user, null);
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        //TODO: Aktywacja mailem
        //TODO: Przypomnienie hasła

        [HttpPost]
        [Route("user/role")]
        public async Task<ActionResult> AddUserToRole([FromBody] UserRoleAddDto addDto)
        {
            if (addDto.UserId <= 0 || string.IsNullOrEmpty(addDto.RoleName))
                return BadRequest();

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == addDto.UserId);

            if (user == null)
                return NotFound();

            var result = await _userManager.AddToRoleAsync(user, addDto.RoleName);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Route("user/role")]
        public async Task<ActionResult> RemoveUserFromRole([FromBody] UserRoleAddDto addDto)
        {
            if (addDto.UserId <= 0 || string.IsNullOrEmpty(addDto.RoleName))
                return BadRequest();

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == addDto.UserId);

            if (user == null)
                return NotFound();

            var result = await _userManager.RemoveFromRoleAsync(user, addDto.RoleName);

            if (result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("roles")]
        public async Task<ActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesToReturn = _mapper.Map<RoleDto[]>(roles);

            return Ok(rolesToReturn);
        }

        [HttpPost]
        [Route("role")]
        public async Task<ActionResult> AddRole([FromBody]RoleAddDto roleAdd)
        {
            if (string.IsNullOrEmpty(roleAdd.Name))
                return BadRequest();

            var result = await _roleManager.CreateAsync(new Role {Name = roleAdd.Name});

            if(result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        [HttpDelete]
        [Route("role")]
        public async Task<ActionResult> RemoveRole([FromBody]RoleAddDto roleRemove)
        {
            if (string.IsNullOrEmpty(roleRemove.Name))
                return BadRequest();

            var role = await _roleManager.Roles
                .FirstOrDefaultAsync(r => r.Name.ToUpper() == roleRemove.Name.ToUpper());

            if(role == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);

            if(result.Succeeded)
                return Ok();

            return BadRequest(result.Errors);
        }

        private async Task<bool> UserExists(string username) =>
            await _userManager.Users.AnyAsync(u => u.NormalizedUserName == username.ToUpper());
    }
}