using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFileUploadsRepository : IAsyncRepository<FileUploads, Guid>, IRepository<FileUploads, Guid>
{
}