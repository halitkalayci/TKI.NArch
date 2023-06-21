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

    public Task UserMustExist(User? user)
    {
        if (user == null)
            throw new BusinessException("Böyle bir kullanıcı bulunamadı.");
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
}
