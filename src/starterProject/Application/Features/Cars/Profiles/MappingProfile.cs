using Application.Features.Cars.Commands.Create;
using Application.Features.Cars.Queries.GetDynamic;
using Application.Features.Cars.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Car, GetListCarItemDto>().ReverseMap();
        CreateMap<IPaginate<Car>, GetListResponse<GetListCarItemDto>>().ReverseMap();

        CreateMap<Car, GetDynamicCarQueryResponse>().ReverseMap();
        CreateMap<IPaginate<Car>, GetListResponse<GetDynamicCarQueryResponse>>().ReverseMap();

        CreateMap<Car, CreateCarCommand>().ReverseMap();
        CreateMap<CreatedCarDto, Car>().ReverseMap();
    }
}
