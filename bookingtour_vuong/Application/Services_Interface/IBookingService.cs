using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDTO>> GetAllAsync();
        Task<BookingDTO?> GetByIdAsync(Guid id);
        Task AddAsync(BookingDTO dto);
        Task UpdateAsync(BookingDTO dto);
        Task DeleteAsync(Guid id);
    }
}
