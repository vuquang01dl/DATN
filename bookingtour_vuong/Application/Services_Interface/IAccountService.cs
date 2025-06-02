using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDTO>> GetAllAsync();
        Task<AccountDTO?> GetByEmailAsync(string email);
        Task RegisterAsync(RegisterDTO dto);
        Task<string?> LoginAsync(LoginDTO dto);
        Task ToggleStatusAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
