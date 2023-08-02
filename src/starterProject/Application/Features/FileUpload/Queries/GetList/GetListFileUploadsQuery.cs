using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.FileUpload.Queries.GetList;

public class GetListFileUploadsQuery : IRequest<GetListResponse<GetListFileUploadsListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFileUploadsQueryHandler : IRequestHandler<GetListFileUploadsQuery, GetListResponse<GetListFileUploadsListItemDto>>
    {
        private readonly IFileUploadsRepository _fileUploadsRepository;
        private readonly IMapper _mapper;

        public GetListFileUploadsQueryHandler(IFileUploadsRepository fileUploadsRepository, IMapper mapper)
        {
            _fileUploadsRepository = fileUploadsRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFileUploadsListItemDto>> Handle(GetListFileUploadsQuery request, CancellationToken cancellationToken)
        {
            IPaginate<FileUploads> fileUploads = await _fileUploadsRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFileUploadsListItemDto> response = _mapper.Map<GetListResponse<GetListFileUploadsListItemDto>>(fileUploads);
            return response;
        }
    }
}