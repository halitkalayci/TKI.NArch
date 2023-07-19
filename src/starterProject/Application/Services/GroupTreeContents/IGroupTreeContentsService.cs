using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.GroupTreeContents;

public interface IGroupTreeContentsService
{
    Task<GroupTreeContent?> GetAsync(
        Expression<Func<GroupTreeContent, bool>> predicate,
        Func<IQueryable<GroupTreeContent>, IIncludableQueryable<GroupTreeContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<GroupTreeContent>?> GetListAsync(
        Expression<Func<GroupTreeContent, bool>>? predicate = null,
        Func<IQueryable<GroupTreeContent>, IOrderedQueryable<GroupTreeContent>>? orderBy = null,
        Func<IQueryable<GroupTreeContent>, IIncludableQueryable<GroupTreeContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<GroupTreeContent> AddAsync(GroupTreeContent groupTreeContent);
    Task<GroupTreeContent> UpdateAsync(GroupTreeContent groupTreeContent);
    Task<GroupTreeContent> DeleteAsync(GroupTreeContent groupTreeContent, bool permanent = false);
}
