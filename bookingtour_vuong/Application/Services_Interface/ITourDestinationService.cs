using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface ITourDestinationService
    {
        Task<IEnumerable<TourDestinationDTO>> GetAllAsync();
        Task<TourDestinationDTO?> GetByNamesAsync(string tourName, string destinationName);
        Task AddAsync(TourDestinationDTO dto);
        Task DeleteAsync(string tourName, string destinationName);
    }
}
