using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules;
public class AuthBusinessRules : BaseBusinessRules
{
    private readonly IUserRepository _userRepository;

    public AuthBusinessRules(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public Task UserMustBeActive(User user)
    {
        if (user.Status == false)
            throw new BusinessException("Bu kullanıcı pasife alınmış");
        return Task.CompletedTask;
    }

    public async Task UserWithSameEmailAndAnotherIdShouldNotExist(int userId, string email)
    {
        User? user = await _userRepository.GetAsync(i => i.Email == email && i.Id != userId);
        if (user != null)
            throw new BusinessException("Bu email ile kayıtlı bir kullanıcı zaten mevcut");
    }

    public Task UserMustExist(User? user)
    {
        if (user == null)
            throw new BusinessException("Böyle bir kullanıcı bulunamadı.");
        return Task.CompletedTask;
    }
    public Task UserMustHaveOtp(User? user)
    {
        if (user.AuthenticatorType is Core.Security.Enums.AuthenticatorType.None)
            throw new BusinessException("Kullanıcı hali hazırda otp sahibi değil.");

        return Task.CompletedTask;
    }

    public Task UserPasswordMustMatch(User? user, string password)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new BusinessException("Şifre yanlış");

        return Task.CompletedTask;
    }

    public Task RefreshTokenMustExist(RefreshToken? refreshToken)
    {
        if (refreshToken == null)
            throw new BusinessException("Böyle bir token bulunamadı.");

        return Task.CompletedTask;
    }

    public async Task UserWithSameEmailShouldNotExist(string email)
    {
        User? user = await _userRepository.GetAsync(i => i.Email == email);
        if (user != null)
            throw new BusinessException("Bu email ile kayıtlı bir kullanıcı zaten mevcut");
    }

    public async Task EmailAuthenticatorMustExist(EmailAuthenticator? emailAuthenticator)
    {
        if (emailAuthenticator == null)
            throw new BusinessException("Böyle bir verification kodu bulunamadı.");
    }
}
