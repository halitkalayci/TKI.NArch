using Application.Features.Auth.Rules;
using Application.Services.UserService;
using Core.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticator;
public class EnableOtpAuthenticatorCommand : IRequest<EnableOtpAuthenticatorResponse>
{
    public int UserId { get; set; }

    public class EnableOtpAuthenticatorCommandHandler : IRequestHandler<EnableOtpAuthenticatorCommand, EnableOtpAuthenticatorResponse>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserService _userService;

        public Task<EnableOtpAuthenticatorResponse> Handle(EnableOtpAuthenticatorCommand request, CancellationToken cancellationToken)
        {

        }
    }
}

public class EnableOtpAuthenticatorResponse : IResponse
{
    public string SecurityKey { get; set; }
}