using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourDestinationController : ControllerBase
    {
        private readonly ITourDestinationService _service;

        public TourDestinationController(ITourDestinationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{tourId}/{destinationId}")]
        public async Task<IActionResult> GetByKeys(Guid tourId, Guid destinationId)
        {
            var item = await _service.GetByKeysAsync(tourId, destinationId);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourDestinationDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        [HttpDelete("{tourId}/{destinationId}")]
        public async Task<IActionResult> Delete(Guid tourId, Guid destinationId)
        {
            await _service.DeleteAsync(tourId, destinationId);
            return Ok(new { message = "Deleted" });
        }
    }
}
