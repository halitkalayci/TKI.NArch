using Application.Features.Auth.Queries.GetRolesQuery;
using Application.Features.Auth.Queries.GetUsersQuery;
using AutoMapper;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GetRolesQueryDto, OperationClaim>().ReverseMap();
        CreateMap<GetUsersQueryDto, User>().ReverseMap().ForMember(i=>i.Roles, opt => opt.MapFrom(o=> o.UserOperationClaims.Select(i=>i.OperationClaim).Select(i=>i.Name).ToList()));
    }
}
