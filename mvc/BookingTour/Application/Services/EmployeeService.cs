﻿using Application.DTOs.EmployeeDTOs;
using Application.Services_Interface;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateAsync(EmployeeCreationDto dto)
        {
            var employee = new Employee()
            {
                EmployeeID = dto.AccountID,
                FirstName = dto.FirstName.Trim(),
                LastName = dto.LastName.Trim(),
                Position = dto.Position.Trim(),
                Address = dto.Address.Trim(),
                Phone = dto.Phone.Trim(),
                Email = dto.Email.Trim(),
                AccountID = dto.AccountID,
                Image = dto.Image ?? "User_default.jpg"
            };
            await _employeeRepository.AddAsync(employee);
            return employee;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllWithOutTour(Guid TourID)
        {
            return await _employeeRepository.GetAllWithOutTour(TourID);
        }

        public async Task<Employee> GetById(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<Employee> GetByUserID(Guid UserID)
        {
            return await _employeeRepository.GetByUserID(UserID);
        }

        public async Task<String> GetEmployeeNameById(Guid id)
        {
            return await _employeeRepository.GetNameByIdAsync(id);
        }

        public async Task<String> GetEmployeePositionById(Guid id)
        {
            return await _employeeRepository.GetPositionByIdAsync(id);
        }

        public async Task UpdateAsync(Guid customer_id, EmployeeUpdateDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(customer_id);
            if (employee != null)
            {
                employee.FirstName = dto.FirstName;
                employee.LastName = dto.LastName;
                employee.Position = dto.Position;
                employee.Address = dto.Address;
                employee.Phone = dto.Phone;
                employee.Image = dto.Image ?? "User_default.jpg";
                await _employeeRepository.UpdateAsync(employee);
            }
        }
    }
}
