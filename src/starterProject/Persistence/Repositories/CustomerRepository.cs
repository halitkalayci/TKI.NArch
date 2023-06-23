using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CustomerRepository : EfRepositoryBase<Customer, int, BaseDbContext>, ICustomerRepository
{
    public CustomerRepository(BaseDbContext context) : base(context)
    {
    }
}