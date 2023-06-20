using Application.Features.Cars.Queries.GetList;
using Core.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private IMediator Mediator;

    public CarsController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarQuery getListCarQuery = new()
        {
            PageRequest = pageRequest
        };
        var response = Mediator.Send(getListCarQuery);
        return Ok(response);
    }
}
