using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITourStatusLogRepository
    {
        Task AddAsync(TourStatusLog log);
        Task<IEnumerable<TourStatusLog>> GetByTourIdAsync(Guid tourId);
        Task<IEnumerable<TourStatusLog>> GetByTourNameAsync(string tourName);
    }
}
