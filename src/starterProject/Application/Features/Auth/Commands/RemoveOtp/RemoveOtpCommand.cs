using Application.Features.Auth.Rules;
using Application.Services.UserService;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Enums;
using Core.Mailing;
using Application.Repositories;
using Core.Security.EmailAuthenticator;
using Core.Security.OtpAuthenticator;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Application.Pipelines.Authorization;

namespace Application.Features.Auth.Commands.RemoveOtp;
public class RemoveOtpCommand : IRequest, ISecuredRequest
{
    public int UserId { get; set; }
    public string Code { get; set; }

    public string[] Roles => new string[] { };

    public class RemoveOtpCommandHandler : IRequestHandler<RemoveOtpCommand>
    {
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMailService _mailService;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
        private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
        private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
        private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;
        public RemoveOtpCommandHandler(IUserService userService, AuthBusinessRules authBusinessRules, IMailService mailService, IEmailAuthenticatorRepository emailAuthenticatorRepository, IOtpAuthenticatorRepository otpAuthenticatorRepository, IEmailAuthenticatorHelper emailAuthenticatorHelper, IOtpAuthenticatorHelper otpAuthenticatorHelper)
        {
            _userService = userService;
            _authBusinessRules = authBusinessRules;
            _mailService = mailService;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
            _otpAuthenticatorRepository = otpAuthenticatorRepository;
            _emailAuthenticatorHelper = emailAuthenticatorHelper;
            _otpAuthenticatorHelper = otpAuthenticatorHelper;
        }

        public async Task Handle(RemoveOtpCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetById(request.UserId);
            await _authBusinessRules.UserMustExist(user);
            await _authBusinessRules.UserMustHaveOtp(user);

            if(user.AuthenticatorType is AuthenticatorType.Email && string.IsNullOrEmpty(request.Code))
            {
                // Mail gönder
                EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(i => i.UserId == user.Id && i.IsVerified);
                string code = await _emailAuthenticatorHelper.CreateEmailActivationCode();

                emailAuthenticator.ActivationKey = code;

                await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

                _mailService.SendMail(new Mail()
                {
                    ToList=new List<MimeKit.MailboxAddress>() { new MimeKit.MailboxAddress($"{user.FirstName} {user.LastName}", user.Email)},
                    Subject="TKI - OTP Kaldırma Kodu",
                    HtmlBody=$"TKI'ye hoşgeldiniz, OTP'yi kaldırmak için aşağıdaki kodu kullanınız. <br /> {code}"
                });

                return;
            }
            else
            {
                if(user.AuthenticatorType is AuthenticatorType.Otp)
                {
                    OtpAuthenticator? otpAuthenticator = await _otpAuthenticatorRepository.GetAsync(i => i.UserId == user.Id && i.IsVerified);
                    bool result = await _otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, request.Code);


                    if (!result)
                        throw new BusinessException("OTP Hatalı");

                    user.AuthenticatorType = AuthenticatorType.None;
                    await _userService.Update(user);
                    await _otpAuthenticatorRepository.DeleteAsync(otpAuthenticator);
                }
                else
                {
                    EmailAuthenticator emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(i => i.UserId == user.Id && i.IsVerified);

                    if (emailAuthenticator.ActivationKey != request.Code)
                        throw new BusinessException("OTP Hatalı");

                    user.AuthenticatorType = AuthenticatorType.None;
                    await _userService.Update(user);
                    await _emailAuthenticatorRepository.DeleteAsync(emailAuthenticator);
                }
            }
        }
    }
}
