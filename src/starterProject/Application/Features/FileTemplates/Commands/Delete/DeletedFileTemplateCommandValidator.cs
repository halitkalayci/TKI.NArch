using FluentValidation;

namespace Application.Features.FileTemplates.Commands.Delete;

public class DeleteFileTemplateCommandValidator : AbstractValidator<DeleteFileTemplateCommand>
{
    public DeleteFileTemplateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}