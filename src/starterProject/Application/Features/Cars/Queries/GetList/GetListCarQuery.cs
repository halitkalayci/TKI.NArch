using Application.Repositories;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
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
public class GetListCarQuery : IRequest<GetListResponse<GetListCarItemDto>>
{
    // İstek
    public PageRequest PageRequest { get; set; }
    public class GetListCarQueryHandler : IRequestHandler<GetListCarQuery, GetListResponse<GetListCarItemDto>>
    {
        // Bağımlılıklar
        private ICarRepository _carRepository;

        public GetListCarQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<GetListResponse<GetListCarItemDto>> Handle(GetListCarQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Car> cars = await _carRepository.GetListAsync(index: request.PageRequest.PageIndex, size:request.PageRequest.PageSize);
            
            // AutoMapper
            return null;
        }
    }
}
