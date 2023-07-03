using FluentValidation;

namespace Application.Features.Rentals.Commands.Update;

public class UpdateRentalCommandValidator : AbstractValidator<UpdateRentalCommand>
{
    public UpdateRentalCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.CarId).NotEmpty();
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.RentalStartDate).NotEmpty();
        RuleFor(c => c.RentalEndDate).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}