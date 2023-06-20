using Application.Features.Brands.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Brand, GetListBrandDto>().ReverseMap();
        CreateMap<IPaginate<Brand>, GetListResponse<GetListBrandDto>>().ReverseMap();
    }
}
