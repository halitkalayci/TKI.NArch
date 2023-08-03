using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FileTemplates;

public interface IFileTemplatesService
{
    Task<FileTemplate?> GetAsync(
        Expression<Func<FileTemplate, bool>> predicate,
        Func<IQueryable<FileTemplate>, IIncludableQueryable<FileTemplate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<FileTemplate>?> GetListAsync(
        Expression<Func<FileTemplate, bool>>? predicate = null,
        Func<IQueryable<FileTemplate>, IOrderedQueryable<FileTemplate>>? orderBy = null,
        Func<IQueryable<FileTemplate>, IIncludableQueryable<FileTemplate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<FileTemplate> AddAsync(FileTemplate fileTemplate);
    Task<FileTemplate> UpdateAsync(FileTemplate fileTemplate);
    Task<FileTemplate> DeleteAsync(FileTemplate fileTemplate, bool permanent = false);
}
