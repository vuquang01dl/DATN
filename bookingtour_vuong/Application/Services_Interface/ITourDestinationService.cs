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
        Task<TourDestinationDTO?> GetByKeysAsync(Guid tourId, Guid destinationId);
        Task AddAsync(TourDestinationDTO dto);
        Task DeleteAsync(Guid tourId, Guid destinationId);
    }
}
