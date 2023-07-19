using Application.Features.GroupTreeContents.Commands.Create;
using Application.Features.GroupTreeContents.Commands.Delete;
using Application.Features.GroupTreeContents.Commands.Update;
using Application.Features.GroupTreeContents.Queries.GetAll;
using Application.Features.GroupTreeContents.Queries.GetById;
using Application.Features.GroupTreeContents.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupTreeContentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateGroupTreeContentCommand createGroupTreeContentCommand)
    {
        CreatedGroupTreeContentResponse response = await Mediator.Send(createGroupTreeContentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateGroupTreeContentCommand updateGroupTreeContentCommand)
    {
        UpdatedGroupTreeContentResponse response = await Mediator.Send(updateGroupTreeContentCommand);

        return Ok(response);
    }
    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        GetAllGroupTreeContentQuery query = new();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        DeletedGroupTreeContentResponse response = await Mediator.Send(new DeleteGroupTreeContentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        GetByIdGroupTreeContentResponse response = await Mediator.Send(new GetByIdGroupTreeContentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListGroupTreeContentQuery getListGroupTreeContentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListGroupTreeContentListItemDto> response = await Mediator.Send(getListGroupTreeContentQuery);
        return Ok(response);
    }
}