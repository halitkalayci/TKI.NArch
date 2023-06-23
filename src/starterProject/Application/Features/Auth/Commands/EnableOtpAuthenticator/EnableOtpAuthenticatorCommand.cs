using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.Authenticator;
using Application.Services.UserService;
using Core.Application.Pipelines.Authorization;
using Core.Application.Responses;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticator;
public class EnableOtpAuthenticatorCommand : IRequest<EnableOtpAuthenticatorResponse>, ISecuredRequest
{
    public int UserId { get; set; }

    public string[] Roles => new string[] { };

    public class EnableOtpAuthenticatorCommandHandler : IRequestHandler<EnableOtpAuthenticatorCommand, EnableOtpAuthenticatorResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserService _userService;
        private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
        private readonly IAuthenticatorService _authenticatorService;

        public EnableOtpAuthenticatorCommandHandler(AuthBusinessRules authBusinessRules, IUserService userService, IOtpAuthenticatorRepository otpAuthenticatorRepository, IAuthenticatorService authenticatorService)
        {
            _authBusinessRules = authBusinessRules;
            _userService = userService;
            _otpAuthenticatorRepository = otpAuthenticatorRepository;
            _authenticatorService = authenticatorService;
        }

        public async Task<EnableOtpAuthenticatorResponse> Handle(EnableOtpAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            // User bulunacak. Varlığı kontrol edilmeli.
            // Userin önceden oluşturulmuş otp'si olmamalı. Ve seçtiği otp türü None olmalı
            // OTP'yi oluştur.
            // Oluşturulan otp'nin secret keyini string olarak dönücez.

            User? user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserMustExist(user);
            //todo: Userin önceden oluşturulmuş otp'si olmamalı. Ve seçtiği otp türü None olmalı

            OtpAuthenticator newOtp = await _authenticatorService.CreateOtpAuthenticator(user);

            OtpAuthenticator addedOtp = await _otpAuthenticatorRepository.AddAsync(newOtp);

            EnableOtpAuthenticatorResponse response = new()
            {
                SecurityKey = await _authenticatorService.ConvertSecretKeyToString(addedOtp.SecretKey)
            };
            return response;
        }
    }
}

public class EnableOtpAuthenticatorResponse : IResponse
{
    public string SecurityKey { get; set; }
}