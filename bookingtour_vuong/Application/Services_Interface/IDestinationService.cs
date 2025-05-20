using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationDTO>> GetAllAsync();
        Task<DestinationDTO?> GetByIdAsync(Guid id);
        Task AddAsync(DestinationDTO dto);
        Task UpdateAsync(DestinationDTO dto);
        Task DeleteAsync(Guid id);
    }
}
