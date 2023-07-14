using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries.GetAll;
public class GetAllCarItemQuery : IRequest<List<GetAllCarItemDto>>
{
    // Query'de kullanıcıdan talep edilecek alanlar.
    public class GetAllCarItemQueryHandler : IRequestHandler<GetAllCarItemQuery, List<GetAllCarItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public GetAllCarItemQueryHandler(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }



        // Query handle edilirken kullanılacak bağımlılıklar.
        public async Task<List<GetAllCarItemDto>> Handle(GetAllCarItemQuery request, CancellationToken cancellationToken)
        {
            var carsInDb =_carRepository.Query();
            var carList = await carsInDb.ToListAsync();

            List<GetAllCarItemDto> response = _mapper.Map<List<GetAllCarItemDto>>(carList);

            return response;
        }
    }
}
