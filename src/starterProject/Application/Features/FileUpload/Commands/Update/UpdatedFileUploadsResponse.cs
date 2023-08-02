using Core.Application.Responses;

namespace Application.Features.FileUpload.Commands.Update;

public class UpdatedFileUploadsResponse : IResponse
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Destination { get; set; }
    public string Description { get; set; }
}