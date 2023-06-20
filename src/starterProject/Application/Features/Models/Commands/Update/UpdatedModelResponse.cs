using Core.Application.Responses;

namespace Application.Features.Models.Commands.Update;

public class UpdatedModelResponse : IResponse
{
    public long Id { get; set; }
    public int BrandId { get; set; }
    public string Name { get; set; }
}