using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using Infrastructure.Payment.Adapters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries.GetList;

// IRequest<GeriDönüşTipi>
// IRequestHandler<HandleEdilecekRequest, GeriDönüşTipi>

// Kullanıcıdan talep edilecek bağımlılıklar Query/Command içerisine
// Talepe verilecek cevap için gerekli bağımlılıklar Handler içerisine eklenmeli.
public class GetListCarQuery : IRequest<GetListResponse<GetListCarItemDto>>, ISecuredRequest
{
    // İstek
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new string[] { "Car.Get" };

    public class GetListCarQueryHandler : IRequestHandler<GetListCarQuery, GetListResponse<GetListCarItemDto>>
    {
        // Bağımlılıklar
        private ICarRepository _carRepository;
        private IMapper _mapper;
        private IPosServiceAdapter _posServiceAdapter;

        public GetListCarQueryHandler(ICarRepository carRepository, IMapper mapper, IPosServiceAdapter posServiceAdapter)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _posServiceAdapter = posServiceAdapter;
        }

        public async Task<GetListResponse<GetListCarItemDto>> Handle(GetListCarQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Car> cars = await _carRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
            );

            GetListResponse<GetListCarItemDto> response = _mapper.Map<GetListResponse<GetListCarItemDto>>(cars);
            return response;
        }
    }
}
