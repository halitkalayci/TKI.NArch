using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class GroupTreeContentRepository : EfRepositoryBase<GroupTreeContent, int, BaseDbContext>, IGroupTreeContentRepository
{
    public GroupTreeContentRepository(BaseDbContext context) : base(context)
    {
    }
}