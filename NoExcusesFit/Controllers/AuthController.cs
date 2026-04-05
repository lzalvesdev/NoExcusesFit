using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NoExcusesFit.Domain.DTOs.RefreshToken;
using NoExcusesFit.Domain.DTOs.UserAccount;
using NoExcusesFit.Domain.Interfaces.Business;

namespace NoExcusesFit.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserAccountBusiness _userAccountBusiness;

    public AuthController(IUserAccountBusiness userAccountBusiness)
    {
        _userAccountBusiness = userAccountBusiness;
    }

    [HttpPost("register")]
    [EnableRateLimiting("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        await _userAccountBusiness.Register(request);
        return Created();
    }

    [HttpPost("login")]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var response = await _userAccountBusiness.Login(request);
        return Ok(response);
    }

    [HttpPost("refresh")]
    [EnableRateLimiting("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var response = await _userAccountBusiness.RefreshTokenAsync(request.RefreshToken);
        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
    {
        await _userAccountBusiness.LogoutAsync(request.RefreshToken);
        return NoContent();
    }
}