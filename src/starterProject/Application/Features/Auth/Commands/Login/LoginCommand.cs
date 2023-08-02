using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Authenticator;
using Application.Services.UserService;
using Core.Application.Dtos;
using Core.Mailing;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using MediatR;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        private IMailService _mailService;
        public LoginCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules, IAuthenticatorService authenticatorService, IMailService mailService)
        {
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
            _authenticatorService = authenticatorService;
            _mailService = mailService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           

            User? userToLogin = await _userService.GetByEmail(request.UserForLoginDto.Email);
            
            await _authBusinessRules.UserMustExist(userToLogin);
            await _authBusinessRules.UserMustBeActive(userToLogin);
            await _authBusinessRules.UserPasswordMustMatch(userToLogin, request.UserForLoginDto.Password);

            if (userToLogin.AuthenticatorType is not AuthenticatorType.None)
            {
                if(userToLogin.AuthenticatorType is AuthenticatorType.Email && string.IsNullOrEmpty(request.UserForLoginDto.AuthenticatorCode))
                {
                    await _authenticatorService.SendAuthenticatorCode(userToLogin);

                    return new LoginCommandResponse() { AccessToken = null, RefreshToken = null, RequiredAuthenticatorType = AuthenticatorType.Email };
                }

                await _authenticatorService.VerifyOtpAuthenticator(userToLogin, request.UserForLoginDto.AuthenticatorCode);
            }

            AccessToken accessToken = await _authService.CreateAccessToken(userToLogin);

            RefreshToken refreshToken = await _authService.CreateRefreshToken(userToLogin, request.IPAddress);

            await _authService.AddRefreshToken(refreshToken);

            //var text = System.IO.File.ReadAllText("wwwroot/templates/html/login.html");
            //text = text.Replace("{{name}}", userToLogin.FirstName);
            //text = text.Replace("{{Product Name}}", "TKI");
            //text = text.Replace("{{username}}", userToLogin.Email);

            //var emailList = new List<MailboxAddress>() { new(name: $"{userToLogin.FirstName} {userToLogin.LastName}", userToLogin.Email) };

            //_mailService.SendMail(new Mail()
            //{
            //    ToList = emailList,
            //    Subject = "TKI Rent A Car - Hoşgeldiniz",
            //    HtmlBody = text
            //});


            //var word = System.IO.File.ReadAllText("wwwroot/templates/word/template2.xhtml");
            //word = word.Replace("{{username}}", "Halit");
            //word = word.Replace("{{lastname}}", "Kalaycı");
            //word = word.Replace("{{email}}", "Halit@gmail.com");

            //_mailService.SendMail(new Mail()
            //{
            //    ToList = emailList,
            //    Subject = "TKI Rent A Car - Word",
            //    HtmlBody = word
            //});

            return new LoginCommandResponse { AccessToken=accessToken, RefreshToken=refreshToken, RequiredAuthenticatorType=userToLogin.AuthenticatorType};
        }
    }

}
