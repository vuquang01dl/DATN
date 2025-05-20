using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface ITourHotelService
    {
        Task<IEnumerable<TourHotelDTO>> GetAllAsync();
        Task<TourHotelDTO?> GetByKeysAsync(Guid tourId, Guid hotelId);
        Task AddAsync(TourHotelDTO dto);
        Task DeleteAsync(Guid tourId, Guid hotelId);
    }
}
