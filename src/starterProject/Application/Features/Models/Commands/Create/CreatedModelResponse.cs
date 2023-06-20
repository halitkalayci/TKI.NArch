using Core.Application.Responses;

namespace Application.Features.Models.Commands.Create;

public class CreatedModelResponse : IResponse
{
    public long Id { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; }
}