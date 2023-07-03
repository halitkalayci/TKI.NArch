using Core.Application.Responses;

namespace Application.Features.Rentals.Queries.GetById;

public class GetByIdRentalResponse : IResponse
{
    public long Id { get; set; }
    public long CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}