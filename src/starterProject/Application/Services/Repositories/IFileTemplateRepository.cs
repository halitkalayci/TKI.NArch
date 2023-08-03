using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFileTemplateRepository : IAsyncRepository<FileTemplate, Guid>, IRepository<FileTemplate, Guid>
{
}