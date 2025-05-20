using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITourEmployeeRepository
    {
        Task<IEnumerable<TourEmployee>> GetAllAsync();
        Task<TourEmployee?> GetByKeysAsync(Guid tourId, Guid employeeId);
        Task AddAsync(TourEmployee entity);
        Task DeleteAsync(Guid tourId, Guid employeeId);
        Task UpdateAsync(TourEmployee entity);

    }
}
