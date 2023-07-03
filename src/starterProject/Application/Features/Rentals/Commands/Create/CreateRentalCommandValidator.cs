using FluentValidation;

namespace Application.Features.Rentals.Commands.Create;

public class CreateRentalCommandValidator : AbstractValidator<CreateRentalCommand>
{
    public CreateRentalCommandValidator()
    {
        RuleFor(c => c.CarId).NotEmpty();
        RuleFor(c => c.CustomerId).NotEmpty();
        RuleFor(c => c.RentalStartDate).NotEmpty();
        RuleFor(c => c.RentalEndDate).NotEmpty();
        RuleFor(c => c.ReturnDate).NotEmpty();
    }
}