using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.OtpAuthenticator;

namespace Application.Services.Authenticator;

public class AuthenticatorManager : IAuthenticatorService
{
    private readonly IOtpAuthenticatorRepository _otpAuthenticatorRepository;
    private readonly IOtpAuthenticatorHelper _otpAuthenticatorHelper;
    private readonly IEmailAuthenticatorHelper _emailAuthenticatorHelper;

    public AuthenticatorManager(IOtpAuthenticatorRepository otpAuthenticatorRepository, IOtpAuthenticatorHelper otpAuthenticatorHelper, IEmailAuthenticatorHelper emailAuthenticatorHelper)
    {
        _otpAuthenticatorRepository = otpAuthenticatorRepository;
        _otpAuthenticatorHelper = otpAuthenticatorHelper;
        _emailAuthenticatorHelper = emailAuthenticatorHelper;
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
}