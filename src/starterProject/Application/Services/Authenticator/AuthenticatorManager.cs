using Core.Security.Entities;

namespace Application.Services.Authenticator;

public class AuthenticatorManager : IAuthenticatorService
{
    public Task<string> ConvertSecretKeyToString(byte[] secretKey)
    {

    }
    public Task<OtpAuthenticator> CreateOtpAuthenticator(User user)
    {

    }
}