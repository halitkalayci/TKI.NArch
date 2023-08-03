using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.FileTemplates.Queries.GetList;

public class GetListFileTemplateQuery : IRequest<GetListResponse<GetListFileTemplateListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListFileTemplateQueryHandler : IRequestHandler<GetListFileTemplateQuery, GetListResponse<GetListFileTemplateListItemDto>>
    {
        private readonly IFileTemplateRepository _fileTemplateRepository;
        private readonly IMapper _mapper;

        public GetListFileTemplateQueryHandler(IFileTemplateRepository fileTemplateRepository, IMapper mapper)
        {
            _fileTemplateRepository = fileTemplateRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFileTemplateListItemDto>> Handle(GetListFileTemplateQuery request, CancellationToken cancellationToken)
        {
            IPaginate<FileTemplate> fileTemplates = await _fileTemplateRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListFileTemplateListItemDto> response = _mapper.Map<GetListResponse<GetListFileTemplateListItemDto>>(fileTemplates);
            return response;
        }
    }
}