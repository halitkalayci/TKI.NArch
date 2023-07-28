using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Queries.GetUsersQuery;
public class GetUsersQueryDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}

public class GetUsersQuery : IRequest<List<GetUsersQueryDto>>
{
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
            var dbUsers = await  _userRepository.Query().Include(i => i.UserOperationClaims).ThenInclude(i => i.OperationClaim).ToListAsync();

            List<GetUsersQueryDto> dto = _mapper.Map<List<GetUsersQueryDto>>(dbUsers);
            return dto;
        }
    }
}