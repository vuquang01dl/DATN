using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDTO>> GetAllAsync();
        Task<PaymentDTO?> GetByIdAsync(Guid id);
        Task AddAsync(PaymentDTO dto);
        Task UpdateAsync(PaymentDTO dto);
        Task DeleteAsync(Guid id);
    }
}
