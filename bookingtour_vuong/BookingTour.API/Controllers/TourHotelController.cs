using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TourHotelController : ControllerBase
    {
        private readonly ITourHotelService _service;

        public TourHotelController(ITourHotelService service)
        {
            _service = service;
        }

        [HttpGet("{tourName}/{hotelName}")]
        public async Task<IActionResult> GetByNames(string tourName, string hotelName)
        {
            var item = await _service.GetByNamesAsync(tourName, hotelName);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TourHotelDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        [HttpDelete("{tourName}/{hotelName}")]
        public async Task<IActionResult> Delete(string tourName, string hotelName)
        {
            await _service.DeleteAsync(tourName, hotelName);
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
