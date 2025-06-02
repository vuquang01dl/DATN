using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services_Interface
{
    public interface ITourStatusLogService
    {
        Task AddStatusAsync(TourStatusLogDTO dto);
        Task<IEnumerable<TourStatusLogDTO>> GetStatusLogsByTourAsync(Guid tourId);
        Task<IEnumerable<TourStatusLogDTO>> GetStatusLogsByTourNameAsync(string tourName);

    }
}
