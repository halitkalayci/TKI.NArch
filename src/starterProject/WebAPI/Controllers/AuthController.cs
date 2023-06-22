using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticator;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.RefreshTokenCommand;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.VerifyOtpAuthenticator;
using Core.Application.Dtos;
using Core.Security.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
    {
        LoginCommand command = new LoginCommand()
        {
            UserForLoginDto = userForLoginDto,
            IPAddress = getIpAddress()
        };
        var response = await Mediator.Send(command);
        setRefreshTokenToCookie(response.RefreshToken);
        return Ok(response.ToHttpResponse());
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
    {
        registerCommand.IPAddress = getIpAddress();

        var response = await Mediator.Send(registerCommand);
        setRefreshTokenToCookie(response.RefreshToken);
        return Ok(response.AccessToken);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        RefreshTokenCommand command = new RefreshTokenCommand()
        {
            IPAddress = getIpAddress(),
            RefreshToken = getRefreshTokenFromCookie
        };
        var response = await Mediator.Send(command);
        setRefreshTokenToCookie(response.RefreshToken);
        return Ok(response.AccessToken);
    }

    [HttpPost("enable-otp")]
    public async Task<IActionResult> EnableOtp()
    {
        EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand = new()
        {
            UserId = getAuthenticatedUserId()
        };

        var response = await Mediator.Send(enableOtpAuthenticatorCommand);
        return Ok(response);
    }

    [HttpPost("enable-email-otp")]
    public async Task<IActionResult> EnableEmailOtp()
    {
        EnableEmailAuthenticatorCommand command = new()
        {
            UserId = getAuthenticatedUserId(),
            VerifyEmailUrlPrefix = "https://localhost:7046/api/auth/verify-email-otp"
        };
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody] string code)
    {
        VerifyOtpAuthenticatorCommand verifyOtpAuthenticatorCommand = new()
        {
            UserId = getAuthenticatedUserId(),
            Code=code
        };
        await Mediator.Send(verifyOtpAuthenticatorCommand);
        return Ok();
    }

    private string getRefreshTokenFromCookie => Request.Cookies["refreshToken"];
    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires=DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key:"refreshToken", refreshToken.Token, cookieOptions);
    }
}
