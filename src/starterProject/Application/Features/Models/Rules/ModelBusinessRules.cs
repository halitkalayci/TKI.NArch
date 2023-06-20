using Application.Features.Models.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Models.Rules;

public class ModelBusinessRules : BaseBusinessRules
{
    private readonly IModelRepository _modelRepository;

    public ModelBusinessRules(IModelRepository modelRepository)
    {
        _modelRepository = modelRepository;
    }

    public Task ModelShouldExistWhenSelected(Model? model)
    {
        if (model == null)
            throw new BusinessException(ModelsBusinessMessages.ModelNotExists);
        return Task.CompletedTask;
    }

    public async Task ModelIdShouldExistWhenSelected(long id, CancellationToken cancellationToken)
    {
        Model? model = await _modelRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ModelShouldExistWhenSelected(model);
    }
}