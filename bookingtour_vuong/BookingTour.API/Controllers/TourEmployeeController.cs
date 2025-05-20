using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourEmployeeController : ControllerBase
    {
        private readonly ITourEmployeeService _service;

        public TourEmployeeController(ITourEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{tourId}/{employeeId}")]
        public async Task<IActionResult> GetByKeys(Guid tourId, Guid employeeId)
        {
            var item = await _service.GetByKeysAsync(tourId, employeeId);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TourEmployeeDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        [HttpDelete("{tourId}/{employeeId}")]
        public async Task<IActionResult> Delete(Guid tourId, Guid employeeId)
        {
            await _service.DeleteAsync(tourId, employeeId);
            return Ok(new { message = "Deleted" });
        }

        [HttpPut]
        public async Task<IActionResult> Update(TourEmployeeDTO dto)
        {
            await _service.UpdateAsync(dto);
            return Ok(new { message = "Cập nhật phân công thành công" });
        }




    }
}
