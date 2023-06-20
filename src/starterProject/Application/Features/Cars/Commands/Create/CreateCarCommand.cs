using Application.Repositories;
using AutoMapper;
using Domain.Entities;
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

        public CreateCarCommandHandler(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }

        public async Task<CreatedCarDto> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            // Plaka ile zaten bir araç kayıtlı mı?
            Car mappedCar = _mapper.Map<Car>(request);

            Car addedCar = _carRepository.Add(mappedCar);

            CreatedCarDto dto = _mapper.Map<CreatedCarDto>(addedCar);
            return dto;
        }
    }
}
