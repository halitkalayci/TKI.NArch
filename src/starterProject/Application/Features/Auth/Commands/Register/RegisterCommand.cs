using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using Core.Application.Responses;
using Core.Security.Constants;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;
public class RegisterCommand : IRequest<RegisterCommandResponse>, ISecuredRequest,ITransactionalRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string IPAddress { get; set; }
    public List<int> RoleIds { get; set; }

    public string[] Roles => new string[] { GeneralOperationClaims.Admin };

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private IAuthService _authService;
        private IUserService _userService;
        private AuthBusinessRules _businessRules;
        private IMapper _mapper;

        public RegisterCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules businessRules, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _businessRules.UserWithSameEmailShouldNotExist(request.Email);

            User userToAdd = _mapper.Map<User>(request);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            userToAdd.PasswordHash = passwordHash;
            userToAdd.PasswordSalt = passwordSalt;

            User addedUser = await _userService.Add(userToAdd);

            await _authService.AssignRolesToUser(addedUser.Id, request.RoleIds);

            AccessToken accesToken = await _authService.CreateAccessToken(addedUser);
            RefreshToken refreshToken = await _authService.CreateRefreshToken(addedUser, request.IPAddress);

            return new RegisterCommandResponse()
            {
                AccessToken = accesToken,
                RefreshToken = refreshToken
            };
        }
    }
}

public class RegisterCommandResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
