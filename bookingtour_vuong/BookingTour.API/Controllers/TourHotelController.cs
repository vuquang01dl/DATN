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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{tourId}/{hotelId}")]
        public async Task<IActionResult> GetByKeys(Guid tourId, Guid hotelId)
        {
            var item = await _service.GetByKeysAsync(tourId, hotelId);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TourHotelDTO dto)
        {
            await _service.AddAsync(dto);
            return Ok(new { message = "Created" });
        }

        [HttpDelete("{tourId}/{hotelId}")]
        public async Task<IActionResult> Delete(Guid tourId, Guid hotelId)
        {
            await _service.DeleteAsync(tourId, hotelId);
            return Ok(new { message = "Deleted" });
        }
    }
}
