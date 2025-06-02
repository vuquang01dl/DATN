using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TourStatusLogService : ITourStatusLogService
    {
        private readonly ITourStatusLogRepository _repo;

        public TourStatusLogService(ITourStatusLogRepository repo)
        {
            _repo = repo;
        }

        public async Task AddStatusAsync(TourStatusLogDTO dto)
        {
            var log = new TourStatusLog
            {
                Id = Guid.NewGuid(),
                TourId = dto.TourId,
                Status = dto.Status,
                Time = dto.Time == default ? DateTime.Now : dto.Time,
                Note = dto.Note,
                EmployeeId = dto.EmployeeId
            };
            await _repo.AddAsync(log);
        }

        public async Task<IEnumerable<TourStatusLogDTO>> GetStatusLogsByTourAsync(Guid tourId)
        {
            var logs = await _repo.GetByTourIdAsync(tourId);
            return logs.Select(l => new TourStatusLogDTO
            {
                Id = l.Id,
                TourId = l.TourId,
                Status = l.Status,
                Time = l.Time,
                Note = l.Note,
                EmployeeId = l.EmployeeId
            });
        }
        public async Task<IEnumerable<TourStatusLogDTO>> GetStatusLogsByTourNameAsync(string tourName)
        {
            var logs = await _repo.GetByTourNameAsync(tourName);
            return logs.Select(l => new TourStatusLogDTO
            {
                Id = l.Id,
                TourId = l.TourId,
                Status = l.Status,
                Time = l.Time,
                Note = l.Note,
                EmployeeId = l.EmployeeId
            });
        }
    }
}
