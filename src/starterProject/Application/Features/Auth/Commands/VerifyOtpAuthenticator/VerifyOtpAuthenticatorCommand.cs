using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.Authenticator;
using Application.Services.UserService;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.VerifyOtpAuthenticator;
public class VerifyOtpAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string Code { get; set; }

    public class VerifyOtpAuthenticatorCommandHandler : IRequestHandler<VerifyOtpAuthenticatorCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserService _userService;
        private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
        private readonly IAuthenticatorService _authenticatorService;

        public VerifyOtpAuthenticatorCommandHandler(AuthBusinessRules authBusinessRules, IUserService userService, IOtpAuthenticatorRepository otpAuthenticatorRepository, IAuthenticatorService authenticatorService)
        {
            _authBusinessRules = authBusinessRules;
            _userService = userService;
            _otpAuthenticatorRepository = otpAuthenticatorRepository;
            _authenticatorService = authenticatorService;
        }

        public async Task Handle(VerifyOtpAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            OtpAuthenticator otp = await _otpAuthenticatorRepository.GetAsync(i => i.UserId == request.UserId);

            //Todo: otp business rules

            User user = await _userService.GetById(request.UserId);

            otp.IsVerified = true;
            user.AuthenticatorType = Core.Security.Enums.AuthenticatorType.Otp;

           await _authenticatorService.VerifyOtpAuthenticator(user, request.Code);

           await _otpAuthenticatorRepository.UpdateAsync(otp);
           await _userService.Update(user);
        }
    }
}
