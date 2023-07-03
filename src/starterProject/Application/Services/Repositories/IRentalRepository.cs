using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IRentalRepository : IAsyncRepository<Rental, long>, IRepository<Rental, long>
{
}