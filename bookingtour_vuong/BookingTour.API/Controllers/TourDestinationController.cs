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

        [HttpGet("{tourName}/{destinationName}")]
        public async Task<IActionResult> GetByNames(string tourName, string destinationName)
        {
            var item = await _service.GetByNamesAsync(tourName, destinationName);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourDestinationDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        [HttpDelete("{tourName}/{destinationName}")]
        public async Task<IActionResult> Delete(string tourName, string destinationName)
        {
            await _service.DeleteAsync(tourName, destinationName);
            return Ok(new { message = "Deleted" });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

    }
}
