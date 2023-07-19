using Application.Features.GroupTreeContents.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.GroupTreeContents.Queries.GetById;

public class GetByIdGroupTreeContentQuery : IRequest<GetByIdGroupTreeContentResponse>
{
    public int Id { get; set; }

    public class GetByIdGroupTreeContentQueryHandler : IRequestHandler<GetByIdGroupTreeContentQuery, GetByIdGroupTreeContentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupTreeContentRepository _groupTreeContentRepository;
        private readonly GroupTreeContentBusinessRules _groupTreeContentBusinessRules;

        public GetByIdGroupTreeContentQueryHandler(IMapper mapper, IGroupTreeContentRepository groupTreeContentRepository, GroupTreeContentBusinessRules groupTreeContentBusinessRules)
        {
            _mapper = mapper;
            _groupTreeContentRepository = groupTreeContentRepository;
            _groupTreeContentBusinessRules = groupTreeContentBusinessRules;
        }

        public async Task<GetByIdGroupTreeContentResponse> Handle(GetByIdGroupTreeContentQuery request, CancellationToken cancellationToken)
        {
            GroupTreeContent? groupTreeContent = await _groupTreeContentRepository.GetAsync(predicate: gtc => gtc.Id == request.Id, cancellationToken: cancellationToken);
            await _groupTreeContentBusinessRules.GroupTreeContentShouldExistWhenSelected(groupTreeContent);

            GetByIdGroupTreeContentResponse response = _mapper.Map<GetByIdGroupTreeContentResponse>(groupTreeContent);
            return response;
        }
    }
}