using Core.Application.Dtos;

namespace Application.Features.Cars.Commands.Create;

public class CreatedCarDto : IDto
{
    public int Id { get; set; }
    public string Plate { get; set; }
}