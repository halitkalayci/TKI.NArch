using Application.Features.FileUpload.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FileUpload;

public class FileUploadsManager : IFileUploadsService
{
    private readonly IFileUploadsRepository _fileUploadsRepository;
    private readonly FileUploadsBusinessRules _fileUploadsBusinessRules;

    public FileUploadsManager(IFileUploadsRepository fileUploadsRepository, FileUploadsBusinessRules fileUploadsBusinessRules)
    {
        _fileUploadsRepository = fileUploadsRepository;
        _fileUploadsBusinessRules = fileUploadsBusinessRules;
    }

    public async Task<FileUploads?> GetAsync(
        Expression<Func<FileUploads, bool>> predicate,
        Func<IQueryable<FileUploads>, IIncludableQueryable<FileUploads, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        FileUploads? fileUploads = await _fileUploadsRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return fileUploads;
    }

    public async Task<IPaginate<FileUploads>?> GetListAsync(
        Expression<Func<FileUploads, bool>>? predicate = null,
        Func<IQueryable<FileUploads>, IOrderedQueryable<FileUploads>>? orderBy = null,
        Func<IQueryable<FileUploads>, IIncludableQueryable<FileUploads, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<FileUploads> fileUploadsList = await _fileUploadsRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return fileUploadsList;
    }

    public async Task<FileUploads> AddAsync(FileUploads fileUploads)
    {
        FileUploads addedFileUploads = await _fileUploadsRepository.AddAsync(fileUploads);

        return addedFileUploads;
    }

    public async Task<FileUploads> UpdateAsync(FileUploads fileUploads)
    {
        FileUploads updatedFileUploads = await _fileUploadsRepository.UpdateAsync(fileUploads);

        return updatedFileUploads;
    }

    public async Task<FileUploads> DeleteAsync(FileUploads fileUploads, bool permanent = false)
    {
        FileUploads deletedFileUploads = await _fileUploadsRepository.DeleteAsync(fileUploads);

        return deletedFileUploads;
    }

    public string Upload(IFormFile file)
    {
        FileInfo fileInfo = new FileInfo(file.FileName);
        string extension = fileInfo.Extension;
        var fileName = fileInfo.Name + "-" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss-ff") + extension;
        var filePath = Environment.CurrentDirectory + @"\wwwroot\docs\" + fileName;
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fs);
        }
        return "docs/"+fileName;
    }
}
