using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{value}")]
        public async Task<IActionResult> GetByValue(string value)
        {
            var item = await _service.GetByValueAsync(value);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PermissionDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        [HttpPut("{value}")]
        public async Task<IActionResult> Update(string value, PermissionDTO dto)
        {
            if (value != dto.Value) return BadRequest();
            await _service.UpdateAsync(dto);
            return Ok(new { message = "Updated" });
        }

        [HttpDelete("{value}")]
        public async Task<IActionResult> Delete(string value)
        {
            await _service.DeleteAsync(value);
            return Ok(new { message = "Deleted" });
        }
    }
}
