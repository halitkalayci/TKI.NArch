using Core.Application.Responses;

namespace Application.Features.FileUpload.Commands.Delete;

public class DeletedFileUploadsResponse : IResponse
{
    public Guid Id { get; set; }
}