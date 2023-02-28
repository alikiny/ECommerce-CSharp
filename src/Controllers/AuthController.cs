using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Services.AuthService;
using Microsoft.AspNetCore.Authorization;

namespace Backend.src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService service)
        {
            _authService = service;

        }

        [HttpPost("login")]
        public async Task<string> Login(UserAuthDto dto)
        {
            var token = await _authService.LogInAsync(dto.Email, dto.PasswordRaw);
            return token;
        }

        [HttpPost("authenticate")]
        public async Task<User> AuthenticateUser (string token)
        {
            var data = await _authService.AuthenticateUserAsync(token);
            return data;
        }
    }
}