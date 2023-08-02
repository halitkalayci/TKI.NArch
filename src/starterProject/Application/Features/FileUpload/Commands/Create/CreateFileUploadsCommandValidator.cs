using FluentValidation;

namespace Application.Features.FileUpload.Commands.Create;

public class CreateFileUploadsCommandValidator : AbstractValidator<CreateFileUploadsCommand>
{
    public CreateFileUploadsCommandValidator()
    {
        RuleFor(c => c.Description).NotEmpty();
    }
}