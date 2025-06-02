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
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel?> GetByIdAsync(Guid id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task AddAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Hotels.FindAsync(id);
            if (entity != null)
            {
                _context.Hotels.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Hotel?> GetByNameAsync(string hotelName)
        {
            return await _context.Hotels.FirstOrDefaultAsync(h => h.Name == hotelName);
        }
    }
}
