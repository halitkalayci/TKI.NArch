using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authenticator;
public interface IAuthenticatorService
{
    Task<EmailAuthenticator> CreateEmailAuthenticator(User user);
    Task<OtpAuthenticator> CreateOtpAuthenticator(User user);
    Task<string> ConvertSecretKeyToString(byte[] secretKey);
    Task VerifyOtpAuthenticator(User user, string code);
}
