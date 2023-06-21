using Application.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Services.Auth;

public class AuthManager : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;
    private readonly TokenOptions _tokenOptions;
    public Task<AccessToken> CreateAccessToken(User user)
    {
        // TODO:
    }
}
