using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.UserService;
using Core.Application.Responses;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.RefreshTokenCommand;
public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
{
    public string RefreshToken { get; set; }
    public string IPAddress { get; set; }

    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RefreshTokenCommandHandler(IAuthService authService, IUserService userService, AuthBusinessRules authBusinessRules)
        {
            _authService = authService;
            _userService = userService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _authService.GetRefreshToken(request.RefreshToken);
            await _authBusinessRules.RefreshTokenMustExist(refreshToken);

            User user = await _userService.GetById(refreshToken.UserId);

            //TODO: Delete old tokens

            AccessToken accessToken = await _authService.CreateAccessToken(user);
            RefreshToken newRefreshToken = await _authService.CreateRefreshToken(user, request.IPAddress);
            await _authService.AddRefreshToken(newRefreshToken);

            return new RefreshTokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
            // Gelen token ile db'de bir token var mı?
            // Gelen tokenin uyuştuğu userid ile bir db kayıtı var mı?
            // Tokenin süre ve kullanılmış olma durumu?
            // Yeni token oluşturma (refresh-access)
            // return
        }
    }
}


public class RefreshTokenResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
