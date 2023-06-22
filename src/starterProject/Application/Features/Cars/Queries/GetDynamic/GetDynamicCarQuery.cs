using Application.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries.GetDynamic;
public class GetDynamicCarQuery : IRequest<GetListResponse<GetDynamicCarQueryResponse>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetDynamicCarQueryHandler : IRequestHandler<GetDynamicCarQuery, GetListResponse<GetDynamicCarQueryResponse>>
    {
        private ICarRepository _carRepository;
        private IMapper _mapper;

        public GetDynamicCarQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetDynamicCarQueryResponse>> Handle(GetDynamicCarQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Car> cars = await _carRepository
                .GetListByDynamicAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    dynamic: request.DynamicQuery
                );

            GetListResponse<GetDynamicCarQueryResponse> response = _mapper.Map<GetListResponse<GetDynamicCarQueryResponse>>(cars);

            return response;
        }
    }
}
