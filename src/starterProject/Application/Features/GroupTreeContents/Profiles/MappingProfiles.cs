using Application.Features.GroupTreeContents.Commands.Create;
using Application.Features.GroupTreeContents.Commands.Delete;
using Application.Features.GroupTreeContents.Commands.Update;
using Application.Features.GroupTreeContents.Queries.GetById;
using Application.Features.GroupTreeContents.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;
using Application.Features.GroupTreeContents.Queries.GetAll;

namespace Application.Features.GroupTreeContents.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<GroupTreeContent, CreateGroupTreeContentCommand>().ReverseMap();
        CreateMap<GroupTreeContent, CreatedGroupTreeContentResponse>().ReverseMap();
        CreateMap<GroupTreeContent, UpdateGroupTreeContentCommand>().ReverseMap();
        CreateMap<GroupTreeContent, UpdatedGroupTreeContentResponse>().ReverseMap();
        CreateMap<GroupTreeContent, DeleteGroupTreeContentCommand>().ReverseMap();
        CreateMap<GroupTreeContent, DeletedGroupTreeContentResponse>().ReverseMap();
        CreateMap<GroupTreeContent, GetByIdGroupTreeContentResponse>().ReverseMap();
        CreateMap<GroupTreeContent, GetListGroupTreeContentListItemDto>().ReverseMap();
        CreateMap<IPaginate<GroupTreeContent>, GetListResponse<GetListGroupTreeContentListItemDto>>().ReverseMap();

        CreateMap<GetAllGroupTreeContentDto, GroupTreeContent>().ReverseMap()
            .ForMember(member => member.Roles, opt => opt.MapFrom(i=> i.GroupTreeContentOperationClaims.Select(i=>i.OperationClaim).Select(i=>i.Name).ToList()));
    }
}