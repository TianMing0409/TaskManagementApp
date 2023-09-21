using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.DTOS;
using TaskManagementApp.Application.Interfaces;

namespace TaskManagementApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            bool result = await _authService.RegisterAsync(model.Email, model.Password, model.FirstName, model.LastName);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Registration failed: Email already exists.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            string token = await _authService.AuthenticateAsync(model.Email, model.Password);
            if(token != null)
            {
                return Ok(new { Token = token });
            }
            return Unauthorized("Authentication failed: invalid credentials.");
        }
    }
}
