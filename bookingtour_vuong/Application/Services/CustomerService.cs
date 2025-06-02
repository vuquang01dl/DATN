using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IAccountRepository _accountRepo;

        public CustomerService(ICustomerRepository repo, IAccountRepository accountRepo)
        {
            _repo = repo;
            _accountRepo = accountRepo;
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
                Image = c.Image
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
                Image = c.Image
            };
        }

        public async Task AddAsync(CustomerDTO dto)
        {
            var email = dto.Email.Trim().ToLower(); // ✅ CHUẨN HÓA EMAIL

            var acc = await _accountRepo.GetByEmailAsync(email);
            if (acc == null)
                throw new Exception("❌ Không tìm thấy tài khoản với email này.");

            var entity = new Customer
            {
                CustomerID = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = email, // ✅ sử dụng bản đã chuẩn hóa
                Address = dto.Address,
                Phone = dto.Phone,
                AccountID = acc.AccountID
            };

            await _repo.AddAsync(entity);
        }
        public async Task<CustomerDTO?> GetByEmailAsync(string email)
        {
            var entity = await _repo.GetByEmailAsync(email);
            if (entity == null) return null;

            // Mapping sang DTO (sửa lại nếu bạn dùng AutoMapper)
            return new CustomerDTO
            {
                CustomerID = entity.CustomerID,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Address = entity.Address,
                Phone = entity.Phone
            };
        }
        public async Task UpdateAsync(CustomerDTO dto)
        {
            var email = dto.Email.Trim().ToLower();
            var acc = await _accountRepo.GetByEmailAsync(email);

            if (acc == null)
                throw new Exception("❌ Không tìm thấy tài khoản với email này.");

            var entity = new Customer
            {
                CustomerID = dto.CustomerID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = email,
                Address = dto.Address,
                Phone = dto.Phone,
                Image = dto.Image,
                AccountID = acc.AccountID
            };

            await _repo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _repo.GetByIdAsync(id);
            if (customer == null) return;

            var email = customer.Email.Trim().ToLower();
            var acc = await _accountRepo.GetByEmailAsync(email);

            if (acc == null)
                throw new Exception("❌ Không tìm thấy tài khoản, không thể xoá khách hàng.");

            await _repo.DeleteAsync(id);
        }
    }
}
