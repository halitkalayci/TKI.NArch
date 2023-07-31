using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.Auth;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.UpdateUser;
public class UpdateUserCommand : IRequest, ISecuredRequest, ITransactionalRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public int Id { get; set; }
    public string? Password { get; set; }
    public List<int> RoleIds { get; set; }

    public string[] Roles => new string[] { GeneralOperationClaims.Admin };

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private AuthBusinessRules _authBusinessRules;
        private IAuthService _authService;
        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, AuthBusinessRules authBusinessRules, IAuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authBusinessRules = authBusinessRules;
            _authService = authService;
        }

        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User dbUser = await _userRepository.GetAsync(i => i.Id == request.Id);
            await _authBusinessRules.UserMustExist(dbUser);

            await _authBusinessRules.UserWithSameEmailAndAnotherIdShouldNotExist(dbUser.Id,request.Email);

            // Update'de mapper kullanmamak daha doğru.
            dbUser.Email = request.Email;
            dbUser.FirstName = request.Firstname;
            dbUser.LastName = request.Lastname;
            //dbUser = _mapper.Map<User>(request);

            await _userRepository.UpdateAsync(dbUser);

            // Tüm roller ile bağlantısı kesilecek
            await _authService.RemoveAllRolesFromUser(dbUser.Id);
            // Yeni roller atanacak.
            await _authService.AssignRolesToUser(dbUser.Id, request.RoleIds);
        }
    }
}
