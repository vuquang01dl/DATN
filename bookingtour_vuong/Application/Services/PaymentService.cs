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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;

        public PaymentService(IPaymentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PaymentDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(p => new PaymentDTO
            {
                PaymentID = p.PaymentID,
                Method = p.Method,
                Amount = p.Amount,
                Date = p.Date,
                BookingID = p.BookingID
            });
        }

        public async Task<PaymentDTO?> GetByIdAsync(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return null;
            return new PaymentDTO
            {
                PaymentID = p.PaymentID,
                Method = p.Method,
                Amount = p.Amount,
                Date = p.Date,
                BookingID = p.BookingID
            };
        }

        public async Task AddAsync(PaymentDTO dto)
        {
            var entity = new Payment
            {
                PaymentID = dto.PaymentID,
                Method = dto.Method,
                Amount = dto.Amount,
                Date = dto.Date,
                BookingID = dto.BookingID,
                CustomerID = dto.CustomerID    // ✅ không được thiếu dòng này
            };
            await _repo.AddAsync(entity);
        }


        public async Task UpdateAsync(PaymentDTO dto)
        {
            var entity = new Payment
            {
                PaymentID = dto.PaymentID,
                Method = dto.Method,
                Amount = dto.Amount,
                Date = dto.Date,
                BookingID = dto.BookingID
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
