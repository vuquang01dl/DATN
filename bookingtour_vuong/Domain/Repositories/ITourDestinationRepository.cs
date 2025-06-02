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
        Task<TourDestination?> GetByNamesAsync(string tourName, string destinationName);
        Task AddAsync(string tourName, string destinationName);
        Task DeleteAsync(string tourName, string destinationName);
       
    }

}
