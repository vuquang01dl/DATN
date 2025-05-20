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
    public class TourDestinationService : ITourDestinationService
    {
        private readonly ITourDestinationRepository _repo;

        public TourDestinationService(ITourDestinationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TourDestinationDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(td => new TourDestinationDTO
            {
                TourID = td.TourID,
                DestinationID = td.DestinationID
            });
        }

        public async Task<TourDestinationDTO?> GetByKeysAsync(Guid tourId, Guid destinationId)
        {
            var td = await _repo.GetByKeysAsync(tourId, destinationId);
            if (td == null) return null;

            return new TourDestinationDTO
            {
                TourID = td.TourID,
                DestinationID = td.DestinationID
            };
        }

        public async Task AddAsync(TourDestinationDTO dto)
        {
            var entity = new TourDestination
            {
                TourID = dto.TourID,
                DestinationID = dto.DestinationID
            };
            await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid tourId, Guid destinationId)
        {
            await _repo.DeleteAsync(tourId, destinationId);
        }
    }
}
