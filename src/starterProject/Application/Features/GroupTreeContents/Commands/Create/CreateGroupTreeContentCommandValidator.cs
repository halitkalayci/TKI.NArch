using FluentValidation;

namespace Application.Features.GroupTreeContents.Commands.Create;

public class CreateGroupTreeContentCommandValidator : AbstractValidator<CreateGroupTreeContentCommand>
{
    public CreateGroupTreeContentCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Target).NotEmpty();
        RuleFor(c => c.Icon).NotEmpty();
        RuleFor(c => c.RowOrder).NotEmpty();
        RuleFor(c => c.ShowOnAuth).NotEmpty();
        RuleFor(c => c.HideOnAuth).NotEmpty();
        RuleFor(c => c.ParentId).NotEmpty();
        RuleFor(c => c.Type).NotEmpty();
    }
}