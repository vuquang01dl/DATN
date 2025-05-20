using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
        Task<EmployeeDTO?> GetByIdAsync(Guid id);
        Task AddAsync(EmployeeDTO dto);
        Task UpdateAsync(EmployeeDTO dto);
        Task DeleteAsync(Guid id);
    }
}
