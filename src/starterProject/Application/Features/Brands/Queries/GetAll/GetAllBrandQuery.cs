using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetAll;
public class GetAllBrandQuery : IRequest<List<GetAllBrandQueryDto>>
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, List<GetAllBrandQueryDto>>
    {
        private IBrandRepository _brandRepository;
        private IMapper _mapper;

        public GetAllBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllBrandQueryDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var response = await _brandRepository.Query().ToListAsync();

            List<GetAllBrandQueryDto> dtos = _mapper.Map<List<GetAllBrandQueryDto>>(response);

            return dtos;
        }
    }
}
