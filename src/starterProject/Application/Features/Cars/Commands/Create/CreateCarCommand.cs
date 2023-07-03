using Application.Features.Cars.Rules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Payment.Adapters;
using Infrastructure.Payment.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands.Create;
public class CreateCarCommand : IRequest<CreatedCarDto>
{
    // Kullanıcıdan bu komut için talep ettiğim bilgiler.
    public int Kilometer { get; set; }
    public string Plate { get; set; }

    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, CreatedCarDto>
    {
        // Bağımlılıklar
        private IMapper _mapper;
        private ICarRepository _carRepository;
        private IPosServiceAdapter _posServiceAdapter;
        private CarBusinessRules _carBusinessRules;

        public CreateCarCommandHandler(IMapper mapper, ICarRepository carRepository, CarBusinessRules carBusinessRules, IPosServiceAdapter posServiceAdapter)
        {
            _mapper = mapper;
            _carRepository = carRepository;
            _carBusinessRules = carBusinessRules;
            _posServiceAdapter = posServiceAdapter;
        }

        public async Task<CreatedCarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            await _carBusinessRules.CarWithSamePlateShouldNotExist(request.Plate);

            Car mappedCar = _mapper.Map<Car>(request);

            Car addedCar = _carRepository.Add(mappedCar);

            CreatedCarDto dto = _mapper.Map<CreatedCarDto>(addedCar);

            // Payment alma ihtiyacım
            // Sıkı Bağımlılık
            // StripePayment stripePayment = new StripePayment();
            // stripePayment.MakePayment("123", "612", "12/27");
            _posServiceAdapter.Pay("",123,DateTime.Now);
            return dto;
        }
    }
}
