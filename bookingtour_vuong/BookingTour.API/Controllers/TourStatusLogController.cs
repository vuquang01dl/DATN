using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/tourstatuslog")]
    public class TourStatusLogController : ControllerBase
    {
        private readonly ITourStatusLogService _service;

        public TourStatusLogController(ITourStatusLogService service)
        {
            _service = service;
        }

        // POST: api/tourstatuslog
        [HttpPost]
        public async Task<IActionResult> AddStatus([FromBody] TourStatusLogDTO dto)
        {
            await _service.AddStatusAsync(dto);
            return Ok(new { message = "Trạng thái đã được cập nhật!" });
        }

        // GET: api/tourstatuslog/{tourId}
        [HttpGet("{tourId}")]
        public async Task<IActionResult> GetLogs(Guid tourId)
        {
            var logs = await _service.GetStatusLogsByTourAsync(tourId);
            return Ok(logs);
        }
        [HttpGet("by-name/{tourName}")]
        public async Task<IActionResult> GetLogsByTourName(string tourName)
        {
            var logs = await _service.GetStatusLogsByTourNameAsync(tourName);
            return Ok(logs);
        }
    }
}
