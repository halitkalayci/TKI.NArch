using Core.Application.Responses;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.Login;
public class LoginCommandResponse : IResponse
{
    public AccessToken AccessToken { get; set; }
    public RefreshToken RefreshToken { get; set; }
    public AuthenticatorType RequiredAuthenticatorType { get; set; }
}
