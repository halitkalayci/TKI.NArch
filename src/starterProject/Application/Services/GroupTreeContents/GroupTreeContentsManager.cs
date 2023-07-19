using Application.Features.GroupTreeContents.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.GroupTreeContents;

public class GroupTreeContentsManager : IGroupTreeContentsService
{
    private readonly IGroupTreeContentRepository _groupTreeContentRepository;
    private readonly GroupTreeContentBusinessRules _groupTreeContentBusinessRules;

    public GroupTreeContentsManager(IGroupTreeContentRepository groupTreeContentRepository, GroupTreeContentBusinessRules groupTreeContentBusinessRules)
    {
        _groupTreeContentRepository = groupTreeContentRepository;
        _groupTreeContentBusinessRules = groupTreeContentBusinessRules;
    }

    public async Task<GroupTreeContent?> GetAsync(
        Expression<Func<GroupTreeContent, bool>> predicate,
        Func<IQueryable<GroupTreeContent>, IIncludableQueryable<GroupTreeContent, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        GroupTreeContent? groupTreeContent = await _groupTreeContentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return groupTreeContent;
    }

    public async Task<IPaginate<GroupTreeContent>?> GetListAsync(
        Expression<Func<GroupTreeContent, bool>>? predicate = null,
        Func<IQueryable<GroupTreeContent>, IOrderedQueryable<GroupTreeContent>>? orderBy = null,
        Func<IQueryable<GroupTreeContent>, IIncludableQueryable<GroupTreeContent, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<GroupTreeContent> groupTreeContentList = await _groupTreeContentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return groupTreeContentList;
    }

    public async Task<GroupTreeContent> AddAsync(GroupTreeContent groupTreeContent)
    {
        GroupTreeContent addedGroupTreeContent = await _groupTreeContentRepository.AddAsync(groupTreeContent);

        return addedGroupTreeContent;
    }

    public async Task<GroupTreeContent> UpdateAsync(GroupTreeContent groupTreeContent)
    {
        GroupTreeContent updatedGroupTreeContent = await _groupTreeContentRepository.UpdateAsync(groupTreeContent);

        return updatedGroupTreeContent;
    }

    public async Task<GroupTreeContent> DeleteAsync(GroupTreeContent groupTreeContent, bool permanent = false)
    {
        GroupTreeContent deletedGroupTreeContent = await _groupTreeContentRepository.DeleteAsync(groupTreeContent);

        return deletedGroupTreeContent;
    }
}
