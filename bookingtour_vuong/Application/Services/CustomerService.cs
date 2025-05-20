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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;

        public CustomerService(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(c => new CustomerDTO
            {
                CustomerID = c.CustomerID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                Image = c.Image,
                AccountID = c.AccountID
            });
        }

        public async Task<CustomerDTO?> GetByIdAsync(Guid id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null) return null;
            return new CustomerDTO
            {
                CustomerID = c.CustomerID,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone,
                Image = c.Image,
                AccountID = c.AccountID
            };
        }

        public async Task AddAsync(CustomerDTO dto)
        {
            var entity = new Customer
            {
                CustomerID = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Phone = dto.Phone,
                Image = dto.Image,
                AccountID = dto.AccountID
            };
            await _repo.AddAsync(entity);
        }

        public async Task UpdateAsync(CustomerDTO dto)
        {
            var entity = new Customer
            {
                CustomerID = dto.CustomerID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Phone = dto.Phone,
                Image = dto.Image,
                AccountID = dto.AccountID
            };
            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}
