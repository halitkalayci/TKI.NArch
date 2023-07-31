using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries.GetUsersQuery;

public class GetUsersQuery : IRequest<List<GetUsersQueryDto>>, ISecuredRequest
{
    public string[] Roles => new string[] { GeneralOperationClaims.Admin };

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<GetUsersQueryDto>>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUsersQueryDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var dbUsers = await  _userRepository.Query().Include(i => i.UserOperationClaims.Where(i=>!i.DeletedDate.HasValue)).ThenInclude(i => i.OperationClaim).ToListAsync();

            List<GetUsersQueryDto> dto = _mapper.Map<List<GetUsersQueryDto>>(dbUsers);
            return dto;
        }
    }
}