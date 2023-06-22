using Core.Persistence.Repositories;
using Core.Security.Entities;

namespace Application.Repositories;

public interface IEmailAuthenticatorRepository : IAsyncRepository<EmailAuthenticator, int>, IRepository<EmailAuthenticator, int>
{
}