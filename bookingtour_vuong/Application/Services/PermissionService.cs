using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repo;

        public PermissionService(IPermissionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PermissionDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(p => new PermissionDTO
            {
                Value = p.Value,
                Description = p.Description,
                IsActive = p.IsActive
            });
        }

        public async Task<PermissionDTO?> GetByValueAsync(string value)
        {
            var p = await _repo.GetByValueAsync(value);
            if (p == null) return null;

            return new PermissionDTO
            {
                Value = p.Value,
                Description = p.Description,
                IsActive = p.IsActive
            };
        }

        public async Task AddAsync(PermissionDTO dto)
        {
            var entity = new Permission(dto.Value, dto.Description, dto.IsActive);
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(PermissionDTO dto)
        {
            var entity = new Permission(dto.Value, dto.Description, dto.IsActive);
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string value)
        {
            await _repo.DeleteAsync(value);
        }
    }
}
