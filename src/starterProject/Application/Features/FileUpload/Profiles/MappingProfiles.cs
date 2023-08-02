using Application.Features.FileUpload.Commands.Create;
using Application.Features.FileUpload.Commands.Delete;
using Application.Features.FileUpload.Commands.Update;
using Application.Features.FileUpload.Queries.GetById;
using Application.Features.FileUpload.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.FileUpload.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<FileUploads, CreateFileUploadsCommand>().ReverseMap();
        CreateMap<FileUploads, CreatedFileUploadsResponse>().ReverseMap();
        CreateMap<FileUploads, UpdateFileUploadsCommand>().ReverseMap();
        CreateMap<FileUploads, UpdatedFileUploadsResponse>().ReverseMap();
        CreateMap<FileUploads, DeleteFileUploadsCommand>().ReverseMap();
        CreateMap<FileUploads, DeletedFileUploadsResponse>().ReverseMap();
        CreateMap<FileUploads, GetByIdFileUploadsResponse>().ReverseMap();
        CreateMap<FileUploads, GetListFileUploadsListItemDto>().ReverseMap();
        CreateMap<IPaginate<FileUploads>, GetListResponse<GetListFileUploadsListItemDto>>().ReverseMap();
    }
}