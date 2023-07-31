using Application.Features.Auth.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.UpdateUser;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(i => i.Firstname).NotNull().NotEmpty().MinimumLength(2);
        RuleFor(i => i.Lastname).NotNull().NotEmpty().MinimumLength(2);
        RuleFor(i => i.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(i => i.Password).Must(CustomAuthValidationRules.PasswordShouldBeNullOrMatch).WithMessage("Şifrede en az bir büyük harf, bir rakam, bir sembol bulunmalıdır");
    }
}
