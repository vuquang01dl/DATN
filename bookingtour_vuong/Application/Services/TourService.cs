using Application.DTOs;
using Application.Services_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;



namespace Application.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _repo;

        public TourService(ITourRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TourDTO>> GetAllToursAsync()
        {
            var tours = await _repo.GetAllAsync();
            return tours.Select(t => new TourDTO
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                Location = t.Location,
                StartDate = t.StartDate,
                EndDate = t.EndDate
            });
        }

        public async Task<TourDTO?> GetTourByIdAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) return null;
            return new TourDTO
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Price = t.Price,
                Location = t.Location,
                StartDate = t.StartDate,
                EndDate = t.EndDate
            };
        }

        public async Task AddTourAsync(TourDTO dto)
        {
            var t = new Tour
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            await _repo.AddAsync(t);
        }

        public async Task UpdateTourAsync(TourDTO dto)
        {
            var t = new Tour
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            await _repo.UpdateAsync(t);
        }

        public async Task DeleteTourAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}