using Core.Application.Dtos;

namespace Application.Features.FileTemplates.Queries.GetList;

public class GetListFileTemplateListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}