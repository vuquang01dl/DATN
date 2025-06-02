using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TourStatusLogRepository : ITourStatusLogRepository
    {
        private readonly ApplicationDbContext _context;

        public TourStatusLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TourStatusLog log)
        {
            await _context.TourStatusLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TourStatusLog>> GetByTourIdAsync(Guid tourId)
        {
            return await _context.TourStatusLogs
                .Where(l => l.TourId == tourId)
                .OrderByDescending(l => l.Time)
                .ToListAsync();
        }
        public async Task<IEnumerable<TourStatusLog>> GetByTourNameAsync(string tourName)
        {
            return await _context.TourStatusLogs
                .Include(l => l.Tour)
                .Where(l => l.Tour.Name == tourName)
                .OrderByDescending(l => l.Time)
                .ToListAsync();
        }
    }
}
