using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repo;

        public FeedbackService(IFeedbackRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FeedbackDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(f => new FeedbackDTO
            {
                FeedbackID = f.FeedbackID,
                Content = f.Content,
                Date = f.Date,
                CustomerID = f.CustomerID
            });
        }

        public async Task<FeedbackDTO?> GetByIdAsync(Guid id)
        {
            var f = await _repo.GetByIdAsync(id);
            if (f == null) return null;
            return new FeedbackDTO
            {
                FeedbackID = f.FeedbackID,
                Content = f.Content,
                Date = f.Date,
                CustomerID = f.CustomerID
            };
        }

        public async Task AddAsync(FeedbackDTO dto)
        {
            var entity = new Feedback
            {
                FeedbackID = Guid.NewGuid(),
                Content = dto.Content,
                Date = dto.Date,
                CustomerID = dto.CustomerID
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(FeedbackDTO dto)
        {
            var entity = new Feedback
            {
                FeedbackID = dto.FeedbackID,
                Content = dto.Content,
                Date = dto.Date,
                CustomerID = dto.CustomerID
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
