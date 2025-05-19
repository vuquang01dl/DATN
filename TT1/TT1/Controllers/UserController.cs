using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThucTapW1._1.Payloads.DataRequests;
using TT1.Entities;
using TT1.Payloads.DataRequests;
using TT1.Services.Interfaces;

namespace TT1.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
      

            private readonly IUserService _userService;

            public UserController(IUserService userService)
            {
                _userService = userService;
            }

            [HttpPost("/api/auth/register")]
            public IActionResult Register([FromForm] Request_Register request)
            {
                return Ok(_userService.Register(request));  
            }
            [HttpPost("/api/auth/login")]
            public IActionResult Login([FromForm] Request_Login request)
            {
                return Ok(_userService.Login(request));
            }
            [HttpGet("PhanQuyen")]
            [Authorize(Roles = "manager")] 
            public IActionResult GetAllUsers()
            {
                try
                {
                    
                    var users = _userService.GetAll();
                    return Ok(users);
                }
                catch (UnauthorizedAccessException ex)
                {
                    return Forbid(); 
                }
            }

        [HttpPost("forgot")]
        public IActionResult ForgotPassword(Request_ForgotPassword request)
        {
            return Ok(_userService.ForgotPassword(request.email));
        }

        [HttpPost("reset")]
        public IActionResult ResetPassword(Request_ResetPassword request)
        { 
          return Ok(_userService.ResetPassword(request));
        }
        [HttpGet("Tim kiem theo Id")]
        [Authorize(Roles = "manager")] 
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid(); 
            }
        }

        [HttpPut("update/{userId}")]
        [Authorize(Roles = "manager")] 
        public IActionResult UpdateUser(int userId, [FromBody] Request_UpdateUser request)
        {
            try
            {
                var updatedUser = _userService.UpdateUser(userId, request);

                if (updatedUser == null)
                {
                    return NotFound();
                }

                return Ok(updatedUser);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid(); 
            }
        }




    }
    }

