using FluentValidation;

namespace Application.Features.FileUpload.Commands.Update;

public class UpdateFileUploadsCommandValidator : AbstractValidator<UpdateFileUploadsCommand>
{
    public UpdateFileUploadsCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.FileName).NotEmpty();
        RuleFor(c => c.Destination).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}