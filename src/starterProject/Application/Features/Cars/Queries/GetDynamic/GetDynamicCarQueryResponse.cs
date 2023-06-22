using Core.Application.Responses;

namespace Application.Features.Cars.Queries.GetDynamic;

public class GetDynamicCarQueryResponse : IResponse
{
    public int Id { get; set; }
    public string Plate { get; set; }
    public int Kilometer { get; set; }
}
