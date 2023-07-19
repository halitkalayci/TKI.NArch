using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.GroupTreeContents.Queries.GetList;

public class GetListGroupTreeContentListItemDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Target { get; set; }
    public string Icon { get; set; }
    public int RowOrder { get; set; }
    public bool ShowOnAuth { get; set; }
    public bool HideOnAuth { get; set; }
    public int? ParentId { get; set; }
    public GroupTreeContentType Type { get; set; }
}