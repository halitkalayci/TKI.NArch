using Application.Features.GroupTreeContents.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.GroupTreeContents.Rules;

public class GroupTreeContentBusinessRules : BaseBusinessRules
{
    private readonly IGroupTreeContentRepository _groupTreeContentRepository;

    public GroupTreeContentBusinessRules(IGroupTreeContentRepository groupTreeContentRepository)
    {
        _groupTreeContentRepository = groupTreeContentRepository;
    }

    public Task GroupTreeContentShouldExistWhenSelected(GroupTreeContent? groupTreeContent)
    {
        if (groupTreeContent == null)
            throw new BusinessException(GroupTreeContentsBusinessMessages.GroupTreeContentNotExists);
        return Task.CompletedTask;
    }

    public async Task GroupTreeContentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        GroupTreeContent? groupTreeContent = await _groupTreeContentRepository.GetAsync(
            predicate: gtc => gtc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await GroupTreeContentShouldExistWhenSelected(groupTreeContent);
    }
}