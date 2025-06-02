using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Services_Interface
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllAsync();
        Task<CustomerDTO?> GetByIdAsync(Guid id);
        Task AddAsync(CustomerDTO dto);
        Task UpdateAsync(CustomerDTO dto);
        Task DeleteAsync(Guid id);
        Task<CustomerDTO?> GetByEmailAsync(string email);
    }
}
