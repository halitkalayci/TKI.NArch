using Application.Features.Customers.Commands.Create;
using Application.Features.Customers.Commands.Delete;
using Application.Features.Customers.Commands.Update;
using Application.Features.Customers.Queries.GetById;
using Application.Features.Customers.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCustomerCommand createCustomerCommand)
    {
        CreatedCustomerResponse response = await Mediator.Send(createCustomerCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand updateCustomerCommand)
    {
        UpdatedCustomerResponse response = await Mediator.Send(updateCustomerCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedCustomerResponse response = await Mediator.Send(new DeleteCustomerCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdCustomerResponse response = await Mediator.Send(new GetByIdCustomerQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListCustomerQuery getListCustomerQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListCustomerListItemDto> response = await Mediator.Send(getListCustomerQuery);
        return Ok(response);
    }
}