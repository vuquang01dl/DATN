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
                TourName = td.Tour?.Name ?? "",
                DestinationName = td.Destination?.Name ?? ""
            });
        }

        public async Task<TourDestinationDTO?> GetByNamesAsync(string tourName, string destinationName)
        {
            var td = await _repo.GetByNamesAsync(tourName, destinationName);
            if (td == null) return null;

            return new TourDestinationDTO
            {
                TourName = td.Tour?.Name ?? "",
                DestinationName = td.Destination?.Name ?? ""
            };
        }

        public async Task AddAsync(TourDestinationDTO dto)
        {
            await _repo.AddAsync(dto.TourName, dto.DestinationName);
        }

        public async Task DeleteAsync(string tourName, string destinationName)
        {
            await _repo.DeleteAsync(tourName, destinationName);
        }
    }

}
