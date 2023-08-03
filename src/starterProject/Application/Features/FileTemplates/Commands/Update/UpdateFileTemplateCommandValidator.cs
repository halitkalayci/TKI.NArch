using FluentValidation;

namespace Application.Features.FileTemplates.Commands.Update;

public class UpdateFileTemplateCommandValidator : AbstractValidator<UpdateFileTemplateCommand>
{
    public UpdateFileTemplateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}