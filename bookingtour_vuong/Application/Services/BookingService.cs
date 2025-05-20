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
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<BookingDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(b => new BookingDTO
            {
                BookingID = b.BookingID,
                TourID = b.TourID,
                CustomerID = b.CustomerID,
                PaymentID = b.PaymentID,
                Adult = b.Adult,
                Child = b.Child,
                TotalPrice = b.TotalPrice,
                CreateAt = b.CreateAt,
                ModifyAt = b.ModifyAt
            });
        }

        public async Task<BookingDTO?> GetByIdAsync(Guid id)
        {
            var b = await _repo.GetByIdAsync(id);
            if (b == null) return null;
            return new BookingDTO
            {
                BookingID = b.BookingID,
                TourID = b.TourID,
                CustomerID = b.CustomerID,
                PaymentID = b.PaymentID,
                Adult = b.Adult,
                Child = b.Child,
                TotalPrice = b.TotalPrice,
                CreateAt = b.CreateAt,
                ModifyAt = b.ModifyAt
            };
        }

        public async Task AddAsync(BookingDTO dto)
        {
            var entity = new Booking
            {
                BookingID = Guid.NewGuid(),
                TourID = dto.TourID,
                CustomerID = dto.CustomerID,
                PaymentID = dto.PaymentID,
                Adult = dto.Adult,
                Child = dto.Child,
                TotalPrice = dto.TotalPrice,
                CreateAt = DateTime.Now,
                ModifyAt = DateTime.Now
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(BookingDTO dto)
        {
            var entity = new Booking
            {
                BookingID = dto.BookingID,
                TourID = dto.TourID,
                CustomerID = dto.CustomerID,
                PaymentID = dto.PaymentID,
                Adult = dto.Adult,
                Child = dto.Child,
                TotalPrice = dto.TotalPrice,
                CreateAt = dto.CreateAt,
                ModifyAt = DateTime.Now
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
