using Application.Features.Auth.Commands.Login;
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

    private string getRefreshTokenFromCookie => Request.Cookies["refreshToken"];
    private void setRefreshTokenToCookie(RefreshToken refreshToken)
    {
        CookieOptions cookieOptions = new() { HttpOnly = true, Expires=DateTime.UtcNow.AddDays(7) };
        Response.Cookies.Append(key:"refreshToken", refreshToken.Token, cookieOptions);
    }
}
