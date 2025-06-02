using System;
using System.Collections.Generic;
using System.Linq;
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
                BookingId = b.BookingId,
                TourId = b.TourId,
                CustomerId = b.CustomerId,
                PaymentId = b.PaymentId,
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
                BookingId = b.BookingId,
                TourId = b.TourId,
                CustomerId = b.CustomerId,
                PaymentId = b.PaymentId,
                Adult = b.Adult,
                Child = b.Child,
                TotalPrice = b.TotalPrice,
                CreateAt = b.CreateAt,
                ModifyAt = b.ModifyAt
            };
        }

        public async Task AddAsync(BookingDTO dto)
        {
            // Kiểm tra trước nếu cần
            if (dto.BookingId == Guid.Empty) dto.BookingId = Guid.NewGuid();

            // Tạo Booking entity
            var entity = new Booking
            {
                BookingId = dto.BookingId,
                TourId = dto.TourId,
                CustomerId = dto.CustomerId,
                PaymentId = dto.PaymentId, // đảm bảo Payment này đã tồn tại trước đó
                Adult = dto.Adult,
                Child = dto.Child,
                TotalPrice = dto.TotalPrice,
                CreateAt = dto.CreateAt,
                ModifyAt = dto.ModifyAt
            };

            // Lưu booking
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(BookingDTO dto)
        {
            var entity = new Booking
            {
                BookingId = dto.BookingId,
                TourId = dto.TourId,
                CustomerId = dto.CustomerId,
                PaymentId = dto.PaymentId,
                Adult = dto.Adult,
                Child = dto.Child,
                TotalPrice = dto.TotalPrice,
                CreateAt = dto.CreateAt,
                ModifyAt = DateTime.UtcNow
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
