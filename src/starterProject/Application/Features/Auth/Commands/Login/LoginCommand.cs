using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Authenticator;
using Application.Services.UserService;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;
public class LoginCommand : IRequest<LoginCommandResponse>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IPAddress { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private IAuthService _authService;
        private IUserService _userService;
        private AuthBusinessRules _authBusinessRules;
        private IAuthenticatorService _authenticatorService;
        public LoginCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules, IAuthenticatorService authenticatorService)
        {
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            User? userToLogin = await _userService.GetByEmail(request.UserForLoginDto.Email);

            await _authBusinessRules.UserMustExist(userToLogin);

            await _authBusinessRules.UserPasswordMustMatch(userToLogin, request.UserForLoginDto.Password);

            if (userToLogin.AuthenticatorType is not AuthenticatorType.None)
            {
                if(request.UserForLoginDto.AuthenticatorCode is null)
                {
                    // email
                }

                await _authenticatorService.VerifyOtpAuthenticator(userToLogin, request.UserForLoginDto.AuthenticatorCode);
            }

            AccessToken accessToken = await _authService.CreateAccessToken(userToLogin);

            RefreshToken refreshToken = await _authService.CreateRefreshToken(userToLogin, request.IPAddress);

            await _authService.AddRefreshToken(refreshToken);

            return new LoginCommandResponse { AccessToken=accessToken, RefreshToken=refreshToken, RequiredAuthenticatorType=AuthenticatorType.None};
        }
    }

}
