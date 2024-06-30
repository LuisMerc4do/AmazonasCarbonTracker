using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AmazonasCarbonTracker.Dtos;
using AmazonasCarbonTracker.Models;
using AmazonasCarbonTracker.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AmazonasCarbonTracker.Dtos.AccountDto;

namespace AmazonasCarbonTracker.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

                if (user == null)
                {
                    Log.Warning($"Login attempt failed for non-existent user: {loginDto.Email}");
                    return Unauthorized("Invalid Email");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded)
                {
                    Log.Warning($"Login attempt failed for user: {loginDto.Email}");
                    return Unauthorized("Username or Password Incorrect");
                }

                Log.Information($"User logged in successfully: {loginDto.Email}");
                return Ok(new NewUserDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during login process");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingUser = await _userManager.FindByEmailAsync(registerDto.Email.ToLower());
                if (existingUser != null)
                {
                    Log.Warning($"Registration attempt failed - Email already exists: {registerDto.Email}");
                    return Conflict("Email already exists");
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                };

                var createUserResult = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createUserResult.Succeeded)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (!addToRoleResult.Succeeded)
                    {
                        Log.Error($"Failed to assign role to user: {appUser.Email}");
                        return StatusCode(500, "Failed to assign role to user");
                    }

                    Log.Information($"User registered successfully: {registerDto.Email}");
                    return Ok(new NewUserDto
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    });
                }
                else
                {
                    Log.Error($"Failed to create user: {registerDto.Email}");
                    return StatusCode(500, "Failed to create user");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error during registration process");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
