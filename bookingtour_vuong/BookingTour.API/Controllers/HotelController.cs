using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.DTOs.Application.DTOs;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _service;

        public HotelController(IHotelService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HotelDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] HotelDTO dto)
        {
            if (id != dto.HotelID) return BadRequest();
            await _service.UpdateAsync(dto);
            return Ok(new { message = "Updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Deleted" });
        }
    }
}
