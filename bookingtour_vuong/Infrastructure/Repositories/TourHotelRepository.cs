using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TourHotelRepository : ITourHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public TourHotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourHotel>> GetAllAsync()
        {
            return await _context.TourHotels
                .Include(th => th.Tour)
                .Include(th => th.Hotel)
                .ToListAsync();
        }


        public async Task AddAsync(TourHotel entity)
        {
            _context.TourHotels.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid tourId, Guid hotelId)
        {
            var entity = await GetByKeysAsync(tourId, hotelId);
            if (entity != null)
            {
                _context.TourHotels.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TourHotel?> GetByNamesAsync(string tourName, string hotelName)
        {
            return await _context.TourHotels
                .Include(th => th.Tour)
                .Include(th => th.Hotel)
                .FirstOrDefaultAsync(th =>
                    th.Tour.Name == tourName && th.Hotel.Name == hotelName);
        }

        public async Task DeleteAsync(string tourName, string hotelName)
        {
            var entity = await GetByNamesAsync(tourName, hotelName);
            if (entity != null)
            {
                _context.TourHotels.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<TourHotel?> GetByKeysAsync(Guid tourId, Guid hotelId)
        {
            return await _context.TourHotels
                .Include(th => th.Tour)
                .Include(th => th.Hotel)
                .FirstOrDefaultAsync(th => th.TourId == tourId && th.HotelId == hotelId);
        }


    }
}
