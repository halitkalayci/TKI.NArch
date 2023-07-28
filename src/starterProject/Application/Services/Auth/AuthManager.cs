using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;
using Core.Security.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Application.Services.Auth;

public class AuthManager : IAuthService
{
    private readonly IOperationClaimRepository _operationClaimRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly TokenOptions _tokenOptions;

    public AuthManager(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository, IConfiguration configuration, IOperationClaimRepository operationClaimRepository)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenHelper = tokenHelper;
        _userOperationClaimRepository = userOperationClaimRepository;
        _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        _operationClaimRepository = operationClaimRepository;
    }

    public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
    {
        var addedToken = await _refreshTokenRepository.AddAsync(refreshToken);
        return addedToken;
    }

    public async Task AssignRolesToUser(int userId, List<int> roleIds)
    {
        User user = await _userRepository.GetAsync(i => i.Id == userId);
        if (user == null)
            throw new BusinessException("Kullanıcı bulunamadı.");

        List<UserOperationClaim> claims = new List<UserOperationClaim>();
        foreach (int id in roleIds)
        {
            OperationClaim role = await _operationClaimRepository.GetAsync(i=>i.Id == id);
            if (role != null)
            {
                UserOperationClaim claim = new UserOperationClaim(userId, id);
                claims.Add(claim);
            }
        }

        await _userOperationClaimRepository.AddRangeAsync(claims);
    }

    public async Task<AccessToken> CreateAccessToken(User user)
    {

        IList<OperationClaim> userRoles = await _userOperationClaimRepository
            .Query()
            .Where(i => i.UserId == user.Id)
            //.Include(i=>i.OperationClaim)
            .Select(i => new OperationClaim() { Id = i.OperationClaimId, Name = i.OperationClaim.Name })
            .ToListAsync();

        AccessToken accessToken = _tokenHelper.CreateToken(user, userRoles);
        return accessToken;
    }

    // Add işlemini farklı fonksiyona taşıma
    // RefreshTokenDto
    public async Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
    {
        RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(user, ipAddress);

        return refreshToken;
    }

    public async Task<RefreshToken> GetRefreshToken(string token)
    {
        RefreshToken refreshToken = await _refreshTokenRepository.GetAsync(i => i.Token == token);
        return refreshToken;
    }
}
