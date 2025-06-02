using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.DTOs.Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _repo;

        public HotelService(IHotelRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<HotelDTO>> GetAllAsync()
        {
            var hotels = await _repo.GetAllAsync();
            return hotels.Select(h => new HotelDTO
            {
                HotelID = h.HotelID,
                Name = h.Name,
                StarRating = h.StarRating,
                Description = h.Description,
                Image = h.Image,
                Address = h.Address,
                City = h.City,
                District = h.District,
                Ward = h.Ward,
                Phone = h.Phone
            });
        }

        public async Task<HotelDTO?> GetByIdAsync(Guid id)
        {
            var h = await _repo.GetByIdAsync(id);
            if (h == null) return null;
            return new HotelDTO
            {
                HotelID = h.HotelID,
                Name = h.Name,
                StarRating = h.StarRating,
                Description = h.Description,
                Image = h.Image,
                Address = h.Address,
                City = h.City,
                District = h.District,
                Ward = h.Ward,
                Phone = h.Phone
            };
        }
        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task AddAsync(HotelDTO dto)
        {
            var entity = new Hotel
            {
                HotelID = Guid.NewGuid(),
                Name = dto.Name,
                StarRating = dto.StarRating,
                Description = dto.Description,
                Image = dto.Image,
                Address = dto.Address,
                City = dto.City,
                District = dto.District,
                Ward = dto.Ward,
                Phone = dto.Phone
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(HotelDTO dto)
        {
            var entity = new Hotel
            {
                HotelID = dto.HotelID,
                Name = dto.Name,
                StarRating = dto.StarRating,
                Description = dto.Description,
                Image = dto.Image,
                Address = dto.Address,
                City = dto.City,
                District = dto.District,
                Ward = dto.Ward,
                Phone = dto.Phone
            };
            await _repo.UpdateAsync(entity);
        }

    }
}
