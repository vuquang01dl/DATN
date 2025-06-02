using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account);
        Task<Account?> GetByEmailAsync(string email);
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(Guid id);           // ✅ cần cho ToggleStatus & Delete
        Task UpdateAsync(Account acc);                  // ✅ cần cho ToggleStatus
        Task DeleteAsync(Guid id);                      // ✅ cần cho Delete
    }
}
