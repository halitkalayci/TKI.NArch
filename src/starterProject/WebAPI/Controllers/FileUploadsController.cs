using Application.Features.FileUpload.Commands.Create;
using Application.Features.FileUpload.Commands.Delete;
using Application.Features.FileUpload.Commands.Update;
using Application.Features.FileUpload.Queries.GetById;
using Application.Features.FileUpload.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileUploadsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateFileUploadsCommand createFileUploadsCommand)
    {
        createFileUploadsCommand.UserId = getAuthenticatedUserId();
        CreatedFileUploadsResponse response = await Mediator.Send(createFileUploadsCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFileUploadsCommand updateFileUploadsCommand)
    {
        UpdatedFileUploadsResponse response = await Mediator.Send(updateFileUploadsCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFileUploadsResponse response = await Mediator.Send(new DeleteFileUploadsCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFileUploadsResponse response = await Mediator.Send(new GetByIdFileUploadsQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFileUploadsQuery getListFileUploadsQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFileUploadsListItemDto> response = await Mediator.Send(getListFileUploadsQuery);
        return Ok(response);
    }
}