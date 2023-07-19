using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.GroupTreeContents.Queries.GetList;

public class GetListGroupTreeContentQuery : IRequest<GetListResponse<GetListGroupTreeContentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public class GetListGroupTreeContentQueryHandler : IRequestHandler<GetListGroupTreeContentQuery, GetListResponse<GetListGroupTreeContentListItemDto>>
    {
        private readonly IGroupTreeContentRepository _groupTreeContentRepository;
        private readonly IMapper _mapper;

        public GetListGroupTreeContentQueryHandler(IGroupTreeContentRepository groupTreeContentRepository, IMapper mapper)
        {
            _groupTreeContentRepository = groupTreeContentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListGroupTreeContentListItemDto>> Handle(GetListGroupTreeContentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<GroupTreeContent> groupTreeContents = await _groupTreeContentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListGroupTreeContentListItemDto> response = _mapper.Map<GetListResponse<GetListGroupTreeContentListItemDto>>(groupTreeContents);
            return response;
        }
    }
}