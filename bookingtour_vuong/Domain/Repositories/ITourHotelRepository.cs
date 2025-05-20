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
        Task<TourHotel?> GetByKeysAsync(Guid tourId, Guid hotelId);
        Task AddAsync(TourHotel entity);
        Task DeleteAsync(Guid tourId, Guid hotelId);
    }
}
