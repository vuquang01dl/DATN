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
        Task<string?> LoginAsync(LoginDTO dto); // Trả JWT token
        Task RegisterAsync(RegisterDTO dto);
        Task<AccountDTO?> GetByEmailAsync(string email);
        Task<IEnumerable<AccountDTO>> GetAllAsync();
    }
}
