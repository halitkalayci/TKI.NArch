using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.UpdateUser;
using AutoMapper;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User,RegisterCommand>().ReverseMap();
        CreateMap<User, UpdateUserCommand>().ReverseMap();
    }
}
