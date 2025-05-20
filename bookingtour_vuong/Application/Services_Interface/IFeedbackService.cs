using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;

namespace Application.Services_Interface
{
    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackDTO>> GetAllAsync();
        Task<FeedbackDTO?> GetByIdAsync(Guid id);
        Task AddAsync(FeedbackDTO dto);
        Task UpdateAsync(FeedbackDTO dto);
        Task DeleteAsync(Guid id);
    }
}
