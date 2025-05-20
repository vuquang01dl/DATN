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
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _repo;

        public DestinationService(IDestinationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DestinationDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(d => new DestinationDTO
            {
                DestinationID = d.DestinationID,
                Name = d.Name,
                Description = d.Description,
                Location = d.Location
            });
        }

        public async Task<DestinationDTO?> GetByIdAsync(Guid id)
        {
            var d = await _repo.GetByIdAsync(id);
            if (d == null) return null;
            return new DestinationDTO
            {
                DestinationID = d.DestinationID,
                Name = d.Name,
                Description = d.Description,
                Location = d.Location
            };
        }

        public async Task AddAsync(DestinationDTO dto)
        {
            var entity = new Destination
            {
                DestinationID = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(DestinationDTO dto)
        {
            var entity = new Destination
            {
                DestinationID = dto.DestinationID,
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
