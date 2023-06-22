﻿using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authenticator;
public interface IAuthenticatorService
{
    Task<OtpAuthenticator> CreateOtpAuthenticator(User user);
    Task<string> ConvertSecretKeyToString(byte[] secretKey);
}
