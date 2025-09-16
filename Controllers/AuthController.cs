using Microsoft.AspNetCore.Mvc;
using WebsiteQLNhaTro.Models;
using WebsiteQLNhaTro.Services;

namespace WebsiteQLNhaTro.Controllers
{
    // AuthController: Xử lý đăng nhập, trả về JWT token
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // JwtService: Service tạo và xác thực JWT token
        private readonly JwtService _jwtService;
        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        // Login: Nhận LoginRequest, xác thực và trả về LoginResponse chứa JWT token
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Dummy user validation
            // User: Model đại diện cho người dùng
            // LoginRequest: DTO nhận thông tin đăng nhập
            // LoginResponse: DTO trả về token
            if (request.Username == "admin" && request.Password == "admin")
            {
                var user = new User { Username = request.Username };
                var token = _jwtService.GenerateToken(user);
                return Ok(new LoginResponse { Token = token });
            }
            return Unauthorized();
        }
    }
}
