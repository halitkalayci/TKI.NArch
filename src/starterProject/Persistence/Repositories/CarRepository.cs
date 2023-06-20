using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CarRepository : EfRepositoryBase<Car, long, BaseDbContext>, ICarRepository
{
    public CarRepository(BaseDbContext context)
        : base(context) { }
}
