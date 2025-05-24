using Application.DTOs;
using Application.Services_Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourController : ControllerBase
    {
        private readonly ITourService _service;

        public TourController(ITourService service)
        {
            _service = service;
        }

        // GET: api/tour
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tours = await _service.GetAllToursAsync();
            return Ok(tours);
        }

        // GET: api/tour/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tour = await _service.GetTourByIdAsync(id);
            if (tour == null) return NotFound();
            return Ok(tour);
        }

        // POST: api/tour
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourDTO dto)
        {
            await _service.AddTourAsync(dto);
            return Ok(new { message = "Tour created successfully." });
        }

        // PUT: api/tour/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TourDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch");

            await _service.UpdateTourAsync(dto);
            return Ok(new { message = "Tour updated successfully." });
        }

        // DELETE: api/tour/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteTourAsync(id);
            return Ok(new { message = "Tour deleted successfully." });
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] TourStatus newStatus)
        {
            await _service.UpdateStatusAsync(id, newStatus);
            return Ok(new { message = "Status updated" });
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetTourStatuses()
        {
            var statuses = await _service.GetAllTourStatusesAsync(); // bạn cần implement
            return Ok(statuses);
        }


    }
}
