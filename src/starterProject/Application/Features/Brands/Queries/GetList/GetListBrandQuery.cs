using Application.Repositories;
using AutoMapper;
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

namespace Application.Features.Brands.Queries.GetList;
public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandDto>>
    {
        private IMapper _mapper;
        private IBrandRepository _brandRepository;

        public GetListBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<GetListResponse<GetListBrandDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Brand> brands = await _brandRepository.GetListAsync(index:request.PageRequest.PageIndex, size: request.PageRequest.PageSize);

            GetListResponse<GetListBrandDto> response = _mapper.Map<GetListResponse<GetListBrandDto>>(brands);

            return response;
        }
    }
}
