using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITourDestinationRepository
    {
        Task<IEnumerable<TourDestination>> GetAllAsync();
        Task<TourDestination?> GetByKeysAsync(Guid tourId, Guid destinationId);
        Task AddAsync(TourDestination entity);
        Task DeleteAsync(Guid tourId, Guid destinationId);
    }
}
