using FluentValidation;

namespace Application.Features.FileUpload.Commands.Delete;

public class DeleteFileUploadsCommandValidator : AbstractValidator<DeleteFileUploadsCommand>
{
    public DeleteFileUploadsCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}