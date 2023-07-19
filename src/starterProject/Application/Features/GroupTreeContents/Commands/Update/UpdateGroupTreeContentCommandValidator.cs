using FluentValidation;

namespace Application.Features.GroupTreeContents.Commands.Update;

public class UpdateGroupTreeContentCommandValidator : AbstractValidator<UpdateGroupTreeContentCommand>
{
    public UpdateGroupTreeContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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