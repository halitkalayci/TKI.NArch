using Core.Application.Dtos;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelListItemDto : IDto
{
    public long Id { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; }
}