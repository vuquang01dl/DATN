﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Entities;

namespace Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(Guid id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(Guid id);
    }
}
