using Application.Features.FileTemplates.Commands.Create;
using Application.Features.FileTemplates.Commands.Delete;
using Application.Features.FileTemplates.Commands.Update;
using Application.Features.FileTemplates.Queries.GetById;
using Application.Features.FileTemplates.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.FileTemplates.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FileTemplate, CreateFileTemplateCommand>().ReverseMap();
        CreateMap<FileTemplate, CreatedFileTemplateResponse>().ReverseMap();
        CreateMap<FileTemplate, UpdateFileTemplateCommand>().ReverseMap();
        CreateMap<FileTemplate, UpdatedFileTemplateResponse>().ReverseMap();
        CreateMap<FileTemplate, DeleteFileTemplateCommand>().ReverseMap();
        CreateMap<FileTemplate, DeletedFileTemplateResponse>().ReverseMap();
        CreateMap<FileTemplate, GetByIdFileTemplateResponse>().ReverseMap();
        CreateMap<FileTemplate, GetListFileTemplateListItemDto>().ReverseMap();
        CreateMap<IPaginate<FileTemplate>, GetListResponse<GetListFileTemplateListItemDto>>().ReverseMap();
    }
}