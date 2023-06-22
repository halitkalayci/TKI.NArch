using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>(); 

    private IMediator _mediator;

    protected string? getIpAddress()
    {
        // .NET'de gelen isteğin IP'sini alma.

        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];

        return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
    protected int getAuthenticatedUserId()
    {
        int userId = HttpContext.User.GetUserId();
        return userId;
    }
}
