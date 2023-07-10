using Application.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OtpAuthenticatorRepository : EfRepositoryBase<OtpAuthenticator, int, BaseDbContext>, IOtpAuthenticatorRepository
{
    public OtpAuthenticatorRepository(BaseDbContext context)
        : base(context) { }

    public OtpAuthenticator GetLatestAuthenticator(int userId)
    {
        return Context.OtpAuthenticator.OrderByDescending(i => i.Id).FirstOrDefault(i => i.UserId == userId);
    }
}
