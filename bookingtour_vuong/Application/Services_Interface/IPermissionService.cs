using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDTO>> GetAllAsync();
        Task<PermissionDTO?> GetByValueAsync(string value);
        Task AddAsync(PermissionDTO dto);
        Task UpdateAsync(PermissionDTO dto);
        Task DeleteAsync(string value);
    }
}
