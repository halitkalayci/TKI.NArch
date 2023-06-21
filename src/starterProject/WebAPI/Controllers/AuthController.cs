using Application.Features.Auth.Commands.Login;
using Core.Application.Dtos;
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
        return Ok(response);
    }
}
