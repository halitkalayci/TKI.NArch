using Application.Features.FileUpload.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.FileUpload.Rules;

public class FileUploadsBusinessRules : BaseBusinessRules
{
    private readonly IFileUploadsRepository _fileUploadsRepository;

    public FileUploadsBusinessRules(IFileUploadsRepository fileUploadsRepository)
    {
        _fileUploadsRepository = fileUploadsRepository;
    }

    public Task FileUploadsShouldExistWhenSelected(FileUploads? fileUploads)
    {
        if (fileUploads == null)
            throw new BusinessException(FileUploadsBusinessMessages.FileUploadsNotExists);
        return Task.CompletedTask;
    }

    public async Task FileUploadsIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        FileUploads? fileUploads = await _fileUploadsRepository.GetAsync(
            predicate: fu => fu.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FileUploadsShouldExistWhenSelected(fileUploads);
    }
}