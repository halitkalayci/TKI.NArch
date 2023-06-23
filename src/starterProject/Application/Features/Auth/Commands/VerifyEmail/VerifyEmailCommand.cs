using Application.Features.Auth.Rules;
using Application.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.VerifyEmail;
public class VerifyEmailCommand : IRequest, ISecuredRequest
{
    public string ActivationKey { get; set; }

    public string[] Roles => new string[] { };

    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand>
    {
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IEmailAuthenticatorRepository _emailAuthenticatorRepository;

        public VerifyEmailCommandHandler(AuthBusinessRules authBusinessRules, IEmailAuthenticatorRepository emailAuthenticatorRepository)
        {
            _authBusinessRules = authBusinessRules;
            _emailAuthenticatorRepository = emailAuthenticatorRepository;
        }

        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            EmailAuthenticator? emailAuthenticator = await _emailAuthenticatorRepository.GetAsync(i=>i.ActivationKey == request.ActivationKey);

            await _authBusinessRules.EmailAuthenticatorMustExist(emailAuthenticator);

            emailAuthenticator.ActivationKey = null;
            emailAuthenticator.IsVerified = true;

            await _emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }
    }
}
