using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDTO>> GetAllAsync();
        Task<HotelDTO?> GetByIdAsync(Guid id);
        Task AddAsync(HotelDTO dto);
        Task UpdateAsync(HotelDTO dto);
        Task DeleteAsync(Guid id);
    }
}
