using Application.Features.Cars.Constants;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Cars.Rules;
public class CarBusinessRules : BaseBusinessRules
{
    private readonly ICarRepository _carRepository;

    public CarBusinessRules(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }


    public async Task CarWithSamePlateShouldNotExist(string plate)
    {
        Car? car = await _carRepository.GetAsync(i => i.Plate == plate);
        if (car != null)
            throw new BusinessException(CarMessages.CarWithSamePlateAlreadyExists);
        // Magic String
    }
}
