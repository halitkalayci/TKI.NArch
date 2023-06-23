using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.OtpAuthenticator;
using MimeKit;

namespace Application.Services.Authenticator;

public class AuthenticatorManager : IAuthenticatorService
{
    private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
    private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;
    private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;
    private readonly IMailService _mailService;

    public AuthenticatorManager(IOtpAuthenticatorRepository otpAuthenticatorRepository, IOtpAuthenticatorHelper otpAuthenticatorHelper, IEmailAuthenticatorHelper emailAuthenticatorHelper, IEmailAuthenticatorRepository emailAuthenticatorRepository, IMailService mailService)
    {
        _otpAuthenticatorRepository = otpAuthenticatorRepository;
        _otpAuthenticatorHelper = otpAuthenticatorHelper;
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
        _emailAuthenticatorRepository = emailAuthenticatorRepository;
        _mailService = mailService;
    }

    public async Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {
        string result = await _otpAuthenticatorHelper.ConvertSecretKeyToString(secretKey);
        return result;
    }
    public async Task<OtpAuthenticator> CreateOtpAuthenticator(User user)
    {
        OtpAuthenticator otpAuthenticator = new()
        {
            UserId = user.Id,
            SecretKey = await _otpAuthenticatorHelper.GenerateSecretKey(),
            IsVerified = false
        };
        return otpAuthenticator;
    }

    public async Task VerifyOtpAuthenticator(User user, string code)
    {
        if (user.AuthenticatorType == Core.Security.Enums.AuthenticatorType.Email)
            await verifyEmailAuthenticator();
        else if (user.AuthenticatorType == Core.Security.Enums.AuthenticatorType.Otp)
            await verifyOtpAuthenticator(user,code);
    }

    private async Task verifyOtpAuthenticator(User user, string code)
    {
        OtpAuthenticator authenticator = await _otpAuthenticatorRepository.GetAsync(i=>i.UserId==user.Id);

        if (authenticator is null)
            throw new BusinessException("Kullanıcı için bir otp oluşturulmamış.");

        bool result = await _otpAuthenticatorHelper.VerifyCode(authenticator.SecretKey, code);

        if (!result)
            throw new BusinessException("OTP hatalı.");
    }
    private async Task verifyEmailAuthenticator() => throw new NotImplementedException();
    public async Task<EmailAuthenticator> CreateEmailAuthenticator(User user) {

        EmailAuthenticator authenticator = new()
        {
            UserId = user.Id,
            ActivationKey = await _emailAuthenticatorHelper.CreateEmailActivationKey(),
            IsVerified = false
        };
        return authenticator;
    }

    public async Task SendAuthenticatorCode(User user)
    {
        if (user.AuthenticatorType is not Core.Security.Enums.AuthenticatorType.Email)
            throw new BusinessException("Kullanıcı otp olarak email kullanmıyor.");

        EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(i=>i.UserId == user.Id && i.IsVerified);

        if (emailAuthenticator == null)
            throw new BusinessException("Kullanıcının onaylanmış bir emaili yok.");

        // KOD oluştur, mail
        string authenticatorCode = await _emailAuthenticatorHelper.CreateEmailActivationCode();

        var toList = new List<MailboxAddress>()
        {
            new MailboxAddress($"{user.FirstName} {user.LastName}", user.Email)
        };

        emailAuthenticator.ActivationKey = authenticatorCode;
        await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);

        _mailService.SendMail(new Mail()
        {
            ToList = toList,
            Subject = "TKI'ye tekrar hoşgeldiniz",
            HtmlBody = $"TKI'ye hoşgeldiniz giriş yapmak için aşağıdaki kodu kullanabilirsiniz. <br/> {authenticatorCode}"
        });
    }
}