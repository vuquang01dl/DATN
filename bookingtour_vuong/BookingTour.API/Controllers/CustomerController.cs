using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // ✅ Lấy toàn bộ khách hàng
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        // ✅ Lấy theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        // ✅ Thêm khách hàng mới - SỬA TẠI ĐÂY
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDTO dto) // ✅ Bắt buộc có [FromBody]
        {
            if (dto == null)
                return BadRequest(new { message = "Dữ liệu không hợp lệ." });

            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        // ✅ Cập nhật khách hàng
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerDTO dto)
        {
            if (id != dto.CustomerID)
                return BadRequest(new { message = "ID không khớp." });

            await _service.UpdateAsync(dto);
            return Ok(new { message = "Updated" });
        }

        // ✅ Xoá khách hàng
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Deleted" });
        }
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var item = await _service.GetByEmailAsync(email);
            return item == null ? NotFound() : Ok(item);
        }
    }
}
