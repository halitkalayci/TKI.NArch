using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Queries.GetRolesQuery;

public class GetRolesQuery : IRequest<List<GetRolesQueryDto>>, ISecuredRequest
{
    public string[] Roles => new string[] { GeneralOperationClaims.Admin };

    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<GetRolesQueryDto>>
    {
        private IMapper _mapper;
        private IOperationClaimRepository _operationClaimRepository;

        public GetRolesQueryHandler(IMapper mapper, IOperationClaimRepository operationClaimRepository)
        {
            _mapper = mapper;
            _operationClaimRepository = operationClaimRepository;
        }

        public async Task<List<GetRolesQueryDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            List<OperationClaim> rolesInDb = await _operationClaimRepository.Query().ToListAsync();

            List<GetRolesQueryDto> dto = _mapper.Map<List<GetRolesQueryDto>>(rolesInDb);

            return dto;
        }
    }
}
