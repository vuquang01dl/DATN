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
    public class TourHotelService : ITourHotelService
    {
        private readonly ITourHotelRepository _repo;

        public TourHotelService(ITourHotelRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TourHotelDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(th => new TourHotelDTO
            {
                TourID = th.TourID,
                HotelID = th.HotelID
            });
        }

        public async Task<TourHotelDTO?> GetByKeysAsync(Guid tourId, Guid hotelId)
        {
            var th = await _repo.GetByKeysAsync(tourId, hotelId);
            if (th == null) return null;
            return new TourHotelDTO
            {
                TourID = th.TourID,
                HotelID = th.HotelID
            };
        }

        public async Task AddAsync(TourHotelDTO dto)
        {
            var entity = new TourHotel
            {
                TourID = dto.TourID,
                HotelID = dto.HotelID
            };
            await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid tourId, Guid hotelId)
        {
            await _repo.DeleteAsync(tourId, hotelId);
        }
    }
}
