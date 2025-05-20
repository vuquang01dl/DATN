using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface ITourEmployeeService
    {
        Task<IEnumerable<TourEmployeeDTO>> GetAllAsync();
        Task<TourEmployeeDTO?> GetByKeysAsync(Guid tourId, Guid employeeId);
        Task AddAsync(TourEmployeeDTO dto);
        Task DeleteAsync(Guid tourId, Guid employeeId);
        Task UpdateAsync(TourEmployeeDTO dto);

    }
}
