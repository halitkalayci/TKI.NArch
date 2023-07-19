using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IGroupTreeContentRepository : IAsyncRepository<GroupTreeContent, int>, IRepository<GroupTreeContent, int>
{
}