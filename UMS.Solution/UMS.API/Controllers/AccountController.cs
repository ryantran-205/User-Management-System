using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UMS.API.DTOs.Account;
using UMS.API.Models;
using UMS.API.Services;

namespace UMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController :  ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    
    public AccountController(
        JwtService jwtService,
        SignInManager<User> signInManager,
        UserManager<User> userManager)
    {
        _jwtService = jwtService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (user is null)
        {
            return Unauthorized("Invalid username or password");
        }

        if (user.EmailConfirmed != true)
        {
            return Unauthorized("Please confirm your email");
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid username or password");
        }

        return CreateApplicationUserDto(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await CheckEmailExistsAsync(registerDto.Email))
        {
            return BadRequest("Email already exists.Please try another email");
        }

        var userToAdd = new User
        {
            FirstName = registerDto.FirstName.ToLower(),
            LastName = registerDto.LastName.ToLower(),
            UserName = registerDto.Email.ToLower(),
            Email = registerDto.Email.ToLower(),
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(userToAdd, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok("Your account has been created");
    }

    [Authorize]
    [HttpGet("refresh-token")]
    public async Task<ActionResult<UserDto>> RefreshToken()
    {
        var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value!);

        return CreateApplicationUserDto(user!);
    }
    
    #region Private Helper Methods

    private UserDto CreateApplicationUserDto(User user)
    {
        return new UserDto()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Jwt = _jwtService.CreateJwt(user)
        };
    }

    private async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
    }
    #endregion
}