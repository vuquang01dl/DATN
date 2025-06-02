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
        Task<TourHotelDTO?> GetByNamesAsync(string tourName, string hotelName);
        Task AddAsync(TourHotelDTO dto);
        Task DeleteAsync(string tourName, string hotelName);
    }

}
