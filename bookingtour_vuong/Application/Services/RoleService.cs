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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;

        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            var roles = await _repo.GetAllAsync();
            return roles.Select(r => new RoleDTO
            {
                RoleID = r.RoleID,
                Name = r.Name
            });
        }

        public async Task<RoleDTO?> GetByIdAsync(Guid id)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role == null) return null;
            return new RoleDTO
            {
                RoleID = role.RoleID,
                Name = role.Name
            };
        }

        public async Task AddAsync(RoleDTO dto)
        {
            var role = new Role
            {
                RoleID = Guid.NewGuid(),
                Name = dto.Name
            };
            await _repo.AddAsync(role);
        }

        public async Task UpdateAsync(RoleDTO dto)
        {
            var role = new Role
            {
                RoleID = dto.RoleID,
                Name = dto.Name
            };
            await _repo.UpdateAsync(role);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
