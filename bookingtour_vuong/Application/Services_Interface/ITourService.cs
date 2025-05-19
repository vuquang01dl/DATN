using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services_Interface
{
    public interface ITourService
    {
        Task<IEnumerable<TourDTO>> GetAllToursAsync();
        Task<TourDTO?> GetTourByIdAsync(int id);
        Task AddTourAsync(TourDTO dto);
        Task UpdateTourAsync(TourDTO dto);
        Task DeleteTourAsync(int id);
    }
}