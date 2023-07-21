using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetAll;
using Application.Features.Brands.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]PageRequest pageRequest)
    {
        GetListBrandQuery query = new()
        {
            PageRequest = pageRequest
        };
        var response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeleteBrandCommand command = new DeleteBrandCommand()
        {
            Id = id
        };
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }


    [HttpGet("getall")]
    public async Task<IActionResult> GetAllBrands()
    {
        GetAllBrandQuery query = new();
        var response = await Mediator.Send(query);
        return Ok(response);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandCommand command)
    {
        await Mediator.Send(command);
        return Ok();    
    }
}
