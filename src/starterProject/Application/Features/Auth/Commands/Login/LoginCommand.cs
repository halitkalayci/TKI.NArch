using Core.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;
public class LoginCommand : IRequest<LoginCommandResponse>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IPAddress { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        // Bağımlılıklar
        public Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

        }
    }

}
