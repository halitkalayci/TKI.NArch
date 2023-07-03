using Core.Application.Dtos;

namespace Application.Features.Rentals.Queries.GetList;

public class GetListRentalListItemDto : IDto
{
    public long Id { get; set; }
    public long CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}