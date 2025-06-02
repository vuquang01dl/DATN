using Application.DTOs;
using Application.Services_Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingTour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            await _service.RegisterAsync(dto);
            return Ok(new { message = "Registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _service.LoginAsync(dto);
            return token == null ? Unauthorized() : Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            if (email == null) return Unauthorized();

            var acc = await _service.GetByEmailAsync(email);
            return acc == null ? NotFound() : Ok(acc);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }
        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            await _service.ToggleStatusAsync(id);
            return Ok(new { message = "Updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Deleted" });
        }
        [HttpGet("email")]
        public async Task<IActionResult> GetByEmailQuery([FromQuery] string email)
        {
            var acc = await _service.GetByEmailAsync(email);
            return acc == null ? NotFound() : Ok(acc);
        }


    }
}
