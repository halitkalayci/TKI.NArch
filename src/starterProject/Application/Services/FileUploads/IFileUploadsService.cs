using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FileUpload;

public interface IFileUploadsService
{
    Task<FileUploads?> GetAsync(
        Expression<Func<FileUploads, bool>> predicate,
        Func<IQueryable<FileUploads>, IIncludableQueryable<FileUploads, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<FileUploads>?> GetListAsync(
        Expression<Func<FileUploads, bool>>? predicate = null,
        Func<IQueryable<FileUploads>, IOrderedQueryable<FileUploads>>? orderBy = null,
        Func<IQueryable<FileUploads>, IIncludableQueryable<FileUploads, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    string Upload(IFormFile file,string extension="");
    Task<FileUploads> AddAsync(FileUploads fileUploads);
    Task<FileUploads> UpdateAsync(FileUploads fileUploads);
    Task<FileUploads> DeleteAsync(FileUploads fileUploads, bool permanent = false);
}
