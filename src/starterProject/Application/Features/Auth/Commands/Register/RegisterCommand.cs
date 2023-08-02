using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.UserService;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Transaction;
using Core.Application.Responses;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Constants;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Infrastructure.Verification.TCKN;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register;
public class RegisterCommand : IRequest<RegisterCommandResponse>, ITransactionalRequest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime BirthDate { get; set; }
    public string NationalityId { get; set; }
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
        private IVerificationService _verificationService;

        public RegisterCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules businessRules, IMapper mapper, IVerificationService verificationService)
        {
            _authService = authService;
            _userService = userService;
            _businessRules = businessRules;
            _mapper = mapper;
            _verificationService = verificationService;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var verificationResult = await _verificationService.VerifyTCKN(long.Parse(request.NationalityId), request.Firstname, request.Lastname, request.BirthDate.Year);

            if (!verificationResult)
                throw new BusinessException("TC Kimlik numarası doğrulanamadı.");
            await _businessRules.UserWithSameEmailShouldNotExist(request.Email);

            User userToAdd = _mapper.Map<User>(request);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
            userToAdd.PasswordHash = passwordHash;
            userToAdd.PasswordSalt = passwordSalt;
            userToAdd.Status = true;

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
