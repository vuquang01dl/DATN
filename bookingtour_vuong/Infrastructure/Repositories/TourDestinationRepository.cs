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
    public class TourDestinationRepository : ITourDestinationRepository
    {
        private readonly ApplicationDbContext _context;

        public TourDestinationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourDestination>> GetAllAsync()
        {
            return await _context.TourDestinations.ToListAsync();
        }

        public async Task<TourDestination?> GetByKeysAsync(Guid tourId, Guid destinationId)
        {
            return await _context.TourDestinations
                .FirstOrDefaultAsync(td => td.TourID == tourId && td.DestinationID == destinationId);
        }

        public async Task AddAsync(TourDestination entity)
        {
            _context.TourDestinations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid tourId, Guid destinationId)
        {
            var entity = await GetByKeysAsync(tourId, destinationId);
            if (entity != null)
            {
                _context.TourDestinations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
