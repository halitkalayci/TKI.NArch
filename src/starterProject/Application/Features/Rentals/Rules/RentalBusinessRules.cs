using Application.Features.Rentals.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Rentals.Rules;

public class RentalBusinessRules : BaseBusinessRules
{
    private readonly IRentalRepository _rentalRepository;

    public RentalBusinessRules(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public Task RentalShouldExistWhenSelected(Rental? rental)
    {
        if (rental == null)
            throw new BusinessException(RentalsBusinessMessages.RentalNotExists);
        return Task.CompletedTask;
    }

    public async Task RentalIdShouldExistWhenSelected(long id, CancellationToken cancellationToken)
    {
        Rental? rental = await _rentalRepository.GetAsync(
            predicate: r => r.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await RentalShouldExistWhenSelected(rental);
    }
}