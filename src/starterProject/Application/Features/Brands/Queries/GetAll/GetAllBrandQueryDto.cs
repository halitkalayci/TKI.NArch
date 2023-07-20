using Core.Application.Dtos;

namespace Application.Features.Brands.Queries.GetAll;

public class GetAllBrandQueryDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
}
