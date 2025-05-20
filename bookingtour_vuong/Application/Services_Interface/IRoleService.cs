using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO?> GetByIdAsync(Guid id);
        Task AddAsync(RoleDTO dto);
        Task UpdateAsync(RoleDTO dto);
        Task DeleteAsync(Guid id);
    }
}
