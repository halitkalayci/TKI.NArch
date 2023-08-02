using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FileUploadsRepository : EfRepositoryBase<FileUploads, Guid, BaseDbContext>, IFileUploadsRepository
{
    public FileUploadsRepository(BaseDbContext context) : base(context)
    {
    }
}