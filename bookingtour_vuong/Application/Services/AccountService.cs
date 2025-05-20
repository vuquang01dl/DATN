using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly IConfiguration _config;

        public AccountService(IAccountRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        public async Task RegisterAsync(RegisterDTO dto)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var account = new Account
            {
                AccountID = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = hash,
                Role = dto.Role
            };
            await _repo.AddAsync(account);
        }

        public async Task<string?> LoginAsync(LoginDTO dto)
        {
            var acc = await _repo.GetByEmailAsync(dto.Email);
            if (acc == null || !BCrypt.Net.BCrypt.Verify(dto.Password, acc.PasswordHash))
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, acc.Email),
                new Claim(ClaimTypes.Role, acc.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(double.Parse(_config["Jwt:ExpireMinutes"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AccountDTO?> GetByEmailAsync(string email)
        {
            var acc = await _repo.GetByEmailAsync(email);
            if (acc == null) return null;

            return new AccountDTO
            {
                AccountID = acc.AccountID,
                Email = acc.Email,
                Role = acc.Role,
                IsActive = acc.IsActive
            };
        }

        public async Task<IEnumerable<AccountDTO>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(a => new AccountDTO
            {
                AccountID = a.AccountID,
                Email = a.Email,
                Role = a.Role,
                IsActive = a.IsActive
            });
        }
    }
}
