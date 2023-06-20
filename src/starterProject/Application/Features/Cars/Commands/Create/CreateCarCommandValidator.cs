using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.Create;
public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator()
    {
        RuleFor(i => i.Plate).NotNull().NotEmpty().MinimumLength(2);
        RuleFor(i => i.Kilometer).NotNull().Must(i => i > 0);
    }
}
