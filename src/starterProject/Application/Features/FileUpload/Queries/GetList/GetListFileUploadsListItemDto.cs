using Core.Application.Dtos;

namespace Application.Features.FileUpload.Queries.GetList;

public class GetListFileUploadsListItemDto : IDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string Destination { get; set; }
    public string Description { get; set; }
}