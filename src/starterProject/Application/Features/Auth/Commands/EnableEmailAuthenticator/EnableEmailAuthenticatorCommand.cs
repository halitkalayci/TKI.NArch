using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services.Authenticator;
using Application.Services.UserService;
using Core.Mailing;
using Core.Security.Entities;
using MediatR;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Application.Features.Auth.Commands.EnableEmailAuthenticator;
public class EnableEmailAuthenticatorCommand : IRequest
{
    public int UserId { get; set; }
    public string VerifyEmailUrlPrefix { get; set; }

    public class EnableEmailAuthenticatorCommandHandler : IRequestHandler<EnableEmailAuthenticatorCommand>
    {
        private readonly IUserService _userService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IMailService _mailService;
        private readonly AuthBusinessRules _authBusinessRules;
        public EnableEmailAuthenticatorCommandHandler(IUserService userService, IEmailAuthenticatorRepository emailAuthenticatorRepository, IAuthenticatorService authenticatorService, IMailService mailService, AuthBusinessRules authBusinessRules)
        {
            _userService = userService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _authenticatorService = authenticatorService;
            _mailService = mailService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task Handle(EnableEmailAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            // User'ı bul, doğrula
            // User'ın önceden tanımlı otp ayarının olmaması lazım.
            // EmailAuth eklenecek
            // Email olarak kodu gönder.

            User? user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserMustExist(user);
            // todo: user'in önceden bir authincator type'i olmamalı

            user.AuthenticatorType = Core.Security.Enums.AuthenticatorType.Email;
            await _userService.Update(user);

            EmailAuthenticator createdEmailAuthenticator = await _authenticatorService.CreateEmailAuthenticator(user);

            EmailAuthenticator addedEmailAuthenticator = await _emailAuthenticatorRepository.AddAsync(createdEmailAuthenticator);

            var emailList = new List<MailboxAddress>() { new(name: $"{user.FirstName} {user.LastName}", user.Email) };

            _mailService.SendMail(new Mail()
            {
                ToList = emailList,
                Subject = "TKI Rent A Car - Hesabınızı Doğrulayın",
                HtmlBody = $"TKİ'ye hoşgeldiniz. Hesabınızı doğrulamak için bu linke tıklayınız: {request.VerifyEmailUrlPrefix}?ActivationKey={HttpUtility.UrlEncode(addedEmailAuthenticator.ActivationKey)}"
            });    
        }
    }
}
