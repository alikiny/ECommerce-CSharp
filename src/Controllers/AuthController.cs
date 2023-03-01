using Backend.src.Services.AuthService;

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
            var token = await _authService.LogInAsync(dto.Email, dto.Password);
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