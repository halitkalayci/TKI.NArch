using Core.Application.Responses;
using Infrastructure.Payment.Services.Models;
using Iyzipay.Model;

namespace Application.Features.Rentals.Commands.Create;

public class CreatedRentalResponse : IResponse
{
    public long Id { get; set; }
    public long CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentalStartDate { get; set; }
    public DateTime RentalEndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public Payment3DResponseModel Payment { get; set; }
}