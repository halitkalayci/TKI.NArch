using Application.Features.Cars.Commands.Create;
using Application.Features.Cars.Queries.GetDynamic;
using Application.Features.Cars.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : BaseController
{

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCarQuery getListCarQuery = new() { PageRequest = pageRequest };
        var response = await Mediator.Send(getListCarQuery);
        return Ok(response);
    }

    [HttpPost("dynamic")]
    public async Task<IActionResult> GetByDynamic([FromBody] DynamicQuery dynamic, [FromQuery] PageRequest pageRequest)
    {
        GetDynamicCarQuery query = new()
        {
            PageRequest = pageRequest,
            DynamicQuery = dynamic
        };

        var response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCarCommand command)
    {
        var response = await Mediator.Send(command);
        return Ok(response);
    }
}
