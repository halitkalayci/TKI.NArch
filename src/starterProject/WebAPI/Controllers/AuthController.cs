using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticator;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.RefreshTokenCommand;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.RemoveOtp;
using Application.Features.Auth.Commands.VerifyEmail;
using Application.Features.Auth.Commands.VerifyOtpAuthenticator;
using Application.Features.Auth.Queries.GetRolesQuery;
using Application.Features.Auth.Queries.GetUsersQuery;
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

    [HttpGet("verify-email-otp")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string activationKey)
    {
        // VerifyEmailCommand
        VerifyEmailCommand verifyEmailCommand = new VerifyEmailCommand()
        {
            ActivationKey = activationKey
        };
        await Mediator.Send(verifyEmailCommand);
        return Ok();
    }

    [HttpPost("remove-otp")]
    public async Task<IActionResult> RemoveOtp([FromBody] string? code)
    {
        RemoveOtpCommand removeOtpCommand = new RemoveOtpCommand()
        {
            Code = code,
            UserId = getAuthenticatedUserId()
        };
        await Mediator.Send(removeOtpCommand);
        return Ok();
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
            VerifyEmailUrlPrefix = "https://localhost:7206/api/auth/verify-email-otp"
        };
        await Mediator.Send(command);
        return Ok();
    }

    [HttpGet("get-roles")]
    public async Task<IActionResult> GetRoles()
    {
        GetRolesQuery query = new();
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers()
    {
        GetUsersQuery query = new();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromBody]VerifyOtpAuthenticatorCommand otp)
    {
        otp.UserId = getAuthenticatedUserId();
        await Mediator.Send(otp);
        return Ok();
    }

    private string getRefreshTokenFromCookie => Request.Cookies["refreshToken"];
    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        if (refreshToken == null)
            return;
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires=DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key:"refreshToken", refreshToken.Token, cookieOptions);
    }
}
