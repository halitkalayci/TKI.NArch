using FluentValidation;

namespace Application.Features.FileTemplates.Commands.Create;

public class CreateFileTemplateCommandValidator : AbstractValidator<CreateFileTemplateCommand>
{
    public CreateFileTemplateCommandValidator()
    {
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}