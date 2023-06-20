using Core.Application.Responses;

namespace Application.Features.Models.Queries.GetById;

public class GetByIdModelResponse : IResponse
{
    public long Id { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; }
}