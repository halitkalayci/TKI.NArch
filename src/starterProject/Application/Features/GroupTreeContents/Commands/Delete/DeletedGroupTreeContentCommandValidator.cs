using FluentValidation;

namespace Application.Features.GroupTreeContents.Commands.Delete;

public class DeleteGroupTreeContentCommandValidator : AbstractValidator<DeleteGroupTreeContentCommand>
{
    public DeleteGroupTreeContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}