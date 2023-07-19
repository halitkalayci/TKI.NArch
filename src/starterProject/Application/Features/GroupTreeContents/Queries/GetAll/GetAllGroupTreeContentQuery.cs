using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GroupTreeContents.Queries.GetAll;

public class GetAllGroupTreeContentQuery : IRequest<List<GetAllGroupTreeContentDto>>
{
    public class GetAllGroupTreeContentQueryHandler : IRequestHandler<GetAllGroupTreeContentQuery, List<GetAllGroupTreeContentDto>>
    {
        private IGroupTreeContentRepository _groupTreeContentRepository;
        private IMapper _mapper;

        public GetAllGroupTreeContentQueryHandler(IGroupTreeContentRepository groupTreeContentRepository, IMapper mapper)
        {
            _groupTreeContentRepository = groupTreeContentRepository;
            _mapper = mapper;
        }

        public Task<List<GetAllGroupTreeContentDto>> Handle(GetAllGroupTreeContentQuery request, CancellationToken cancellationToken)
        {
            var groupTreeContents = _groupTreeContentRepository.Query().Include(i=>i.GroupTreeContentOperationClaims).ThenInclude(i=>i.OperationClaim).ToList();

            List<GetAllGroupTreeContentDto> response = _mapper.Map<List<GetAllGroupTreeContentDto>>(groupTreeContents);

            return Task.FromResult(response);
        }
    }
}
