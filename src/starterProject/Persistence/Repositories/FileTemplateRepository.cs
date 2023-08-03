using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FileTemplateRepository : EfRepositoryBase<FileTemplate, Guid, BaseDbContext>, IFileTemplateRepository
{
    public FileTemplateRepository(BaseDbContext context) : base(context)
    {
    }
}