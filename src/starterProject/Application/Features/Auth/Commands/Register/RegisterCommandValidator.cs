using Application.Features.Auth.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(i => i.Firstname).NotNull().NotEmpty().MinimumLength(2);
        RuleFor(i => i.Lastname).NotNull().NotEmpty().MinimumLength(2);
        RuleFor(i => i.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(i => i.Password).NotNull().NotEmpty().WithMessage("Şifre zorunludur");
        RuleFor(i => i.Password).Must(CustomAuthValidationRules.PasswordShouldMatchConstraints).WithMessage("Şifrede en az bir büyük harf, bir rakam, bir sembol bulunmalıdır");
    }
}
