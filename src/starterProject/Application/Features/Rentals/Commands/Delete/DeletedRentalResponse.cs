using Core.Application.Responses;

namespace Application.Features.Rentals.Commands.Delete;

public class DeletedRentalResponse : IResponse
{
    public long Id { get; set; }
}