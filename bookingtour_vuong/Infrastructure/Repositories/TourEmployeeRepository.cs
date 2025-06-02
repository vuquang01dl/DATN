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
    public class TourEmployeeRepository : ITourEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public TourEmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourEmployee>> GetAllAsync()
        {
            return await _context.TourEmployees.ToListAsync();
        }

        public async Task<TourEmployee?> GetByKeysAsync(Guid tourId, Guid employeeId)
        {
            return await _context.TourEmployees
                .FirstOrDefaultAsync(te => te.TourId == tourId && te.EmployeeId == employeeId);
        }

        

        public async Task AddAsync(TourEmployee entity)
        {
            _context.TourEmployees.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid tourId, Guid employeeId)
        {
            var entity = await GetByKeysAsync(tourId, employeeId);
            if (entity != null)
            {
                _context.TourEmployees.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(TourEmployee entity)
        {
            _context.TourEmployees.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
