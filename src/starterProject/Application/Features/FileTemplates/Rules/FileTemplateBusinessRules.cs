using Application.Features.FileTemplates.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.FileTemplates.Rules;

public class FileTemplateBusinessRules : BaseBusinessRules
{
    private readonly IFileTemplateRepository _fileTemplateRepository;

    public FileTemplateBusinessRules(IFileTemplateRepository fileTemplateRepository)
    {
        _fileTemplateRepository = fileTemplateRepository;
    }

    public Task FileTemplateShouldExistWhenSelected(FileTemplate? fileTemplate)
    {
        if (fileTemplate == null)
            throw new BusinessException(FileTemplatesBusinessMessages.FileTemplateNotExists);
        return Task.CompletedTask;
    }

    public async Task FileTemplateIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        FileTemplate? fileTemplate = await _fileTemplateRepository.GetAsync(
            predicate: ft => ft.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FileTemplateShouldExistWhenSelected(fileTemplate);
    }
}