using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface ITourHotelRepository
    {
        Task<IEnumerable<TourHotel>> GetAllAsync();
        Task<TourHotel?> GetByNamesAsync(string tourName, string hotelName);
        Task AddAsync(TourHotel entity); // Nên truyền entity với ID, hoặc truyền DTO với tên, tuỳ bạn chọn
        Task DeleteAsync(string tourName, string hotelName);
        Task<TourHotel?> GetByKeysAsync(Guid tourId, Guid hotelId);

    }
}
