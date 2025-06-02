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
            return await _context.TourDestinations
                .Include(td => td.Tour)
                .Include(td => td.Destination)
                .ToListAsync();
        }

        public async Task<TourDestination?> GetByNamesAsync(string tourName, string destinationName)
        {
            return await _context.TourDestinations
                .Include(td => td.Tour)
                .Include(td => td.Destination)
                .FirstOrDefaultAsync(td => td.Tour.Name == tourName && td.Destination.Name == destinationName);
        }

        public async Task AddAsync(string tourName, string destinationName)
        {
            var tour = await _context.Tours.FirstOrDefaultAsync(t => t.Name == tourName);
            var dest = await _context.Destinations.FirstOrDefaultAsync(d => d.Name == destinationName);
            if (tour == null || dest == null)
                throw new Exception("Tour hoặc Destination không tồn tại!");

            var entity = new TourDestination
            {
                TourId = tour.Id,
                DestinationId = dest.DestinationID
            };
            _context.TourDestinations.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string tourName, string destinationName)
        {
            var entity = await GetByNamesAsync(tourName, destinationName);
            if (entity != null)
            {
                _context.TourDestinations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
