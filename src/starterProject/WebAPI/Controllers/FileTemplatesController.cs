using Application.Features.FileTemplates.Commands.Create;
using Application.Features.FileTemplates.Commands.Delete;
using Application.Features.FileTemplates.Commands.Update;
using Application.Features.FileTemplates.Queries.GetById;
using Application.Features.FileTemplates.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileTemplatesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFileTemplateCommand createFileTemplateCommand)
    {
        createFileTemplateCommand.UserId = getAuthenticatedUserId();
        CreatedFileTemplateResponse response = await Mediator.Send(createFileTemplateCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFileTemplateCommand updateFileTemplateCommand)
    {
        UpdatedFileTemplateResponse response = await Mediator.Send(updateFileTemplateCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFileTemplateResponse response = await Mediator.Send(new DeleteFileTemplateCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFileTemplateResponse response = await Mediator.Send(new GetByIdFileTemplateQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFileTemplateQuery getListFileTemplateQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFileTemplateListItemDto> response = await Mediator.Send(getListFileTemplateQuery);
        return Ok(response);
    }
}