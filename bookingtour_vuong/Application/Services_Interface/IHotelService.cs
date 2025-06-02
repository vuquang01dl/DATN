using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Application.DTOs;

namespace Application.Services_Interface
{
    public interface IHotelService
    {
        /// <summary>
        /// Lấy tất cả khách sạn.
        /// </summary>
        Task<IEnumerable<HotelDTO>> GetAllAsync();

        /// <summary>
        /// Lấy khách sạn theo ID.
        /// </summary>
        Task<HotelDTO?> GetByIdAsync(Guid id);

        /// <summary>
        /// Thêm mới khách sạn.
        /// </summary>
        Task AddAsync(HotelDTO dto);

        /// <summary>
        /// Cập nhật thông tin khách sạn.
        /// </summary>
        Task UpdateAsync(HotelDTO dto);

        /// <summary>
        /// Xoá khách sạn theo ID.
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}
