using Application.Features.FileTemplates.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FileTemplates;

public class FileTemplatesManager : IFileTemplatesService
{
    private readonly IFileTemplateRepository _fileTemplateRepository;
    private readonly FileTemplateBusinessRules _fileTemplateBusinessRules;

    public FileTemplatesManager(IFileTemplateRepository fileTemplateRepository, FileTemplateBusinessRules fileTemplateBusinessRules)
    {
        _fileTemplateRepository = fileTemplateRepository;
        _fileTemplateBusinessRules = fileTemplateBusinessRules;
    }

    public async Task<FileTemplate?> GetAsync(
        Expression<Func<FileTemplate, bool>> predicate,
        Func<IQueryable<FileTemplate>, IIncludableQueryable<FileTemplate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        FileTemplate? fileTemplate = await _fileTemplateRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return fileTemplate;
    }

    public async Task<IPaginate<FileTemplate>?> GetListAsync(
        Expression<Func<FileTemplate, bool>>? predicate = null,
        Func<IQueryable<FileTemplate>, IOrderedQueryable<FileTemplate>>? orderBy = null,
        Func<IQueryable<FileTemplate>, IIncludableQueryable<FileTemplate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<FileTemplate> fileTemplateList = await _fileTemplateRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return fileTemplateList;
    }

    public async Task<FileTemplate> AddAsync(FileTemplate fileTemplate)
    {
        FileTemplate addedFileTemplate = await _fileTemplateRepository.AddAsync(fileTemplate);

        return addedFileTemplate;
    }

    public async Task<FileTemplate> UpdateAsync(FileTemplate fileTemplate)
    {
        FileTemplate updatedFileTemplate = await _fileTemplateRepository.UpdateAsync(fileTemplate);

        return updatedFileTemplate;
    }

    public async Task<FileTemplate> DeleteAsync(FileTemplate fileTemplate, bool permanent = false)
    {
        FileTemplate deletedFileTemplate = await _fileTemplateRepository.DeleteAsync(fileTemplate);

        return deletedFileTemplate;
    }
}
