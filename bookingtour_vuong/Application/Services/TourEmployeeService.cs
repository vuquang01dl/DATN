using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class TourEmployeeService : ITourEmployeeService
    {
        private readonly ITourEmployeeRepository _repo;

        public TourEmployeeService(ITourEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TourEmployeeDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(te => new TourEmployeeDTO
            {
                TourId = te.TourId,
                EmployeeId = te.EmployeeId,
                IsLeader = te.IsLeader
            });
        }

        public async Task<TourEmployeeDTO?> GetByKeysAsync(Guid tourId, Guid employeeId)
        {
            var te = await _repo.GetByKeysAsync(tourId, employeeId);
            if (te == null) return null;
            return new TourEmployeeDTO
            {
                TourId = te.TourId,
                EmployeeId = te.EmployeeId,
                IsLeader = te.IsLeader
            };
        }

        public async Task AddAsync(TourEmployeeDTO dto)
        {
            var entity = new TourEmployee
            {
                TourId = dto.TourId,
                EmployeeId = dto.EmployeeId,
                IsLeader = dto.IsLeader
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(TourEmployeeDTO dto)
        {
            var entity = await _repo.GetByKeysAsync(dto.TourId, dto.EmployeeId);
            if (entity != null)
            {
                entity.IsLeader = dto.IsLeader;
                await _repo.UpdateAsync(entity);
            }
        }

        public async Task DeleteAsync(Guid tourId, Guid employeeId)
        {
            await _repo.DeleteAsync(tourId, employeeId);
        }


    }
}
