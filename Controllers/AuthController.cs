using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebsiteQLNhaTro.Models;
using WebsiteQLNhaTro.Services;
using WebsiteQLNhaTro.DTOs;
using System.Security.Claims;

namespace WebsiteQLNhaTro.Controllers
{
    // AuthController: Xử lý đăng nhập, trả về JWT token
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous] // Cho phép truy cập không cần xác thực
    public class AuthController : ControllerBase
    {
        // JwtService: Service tạo và xác thực JWT token
        private readonly JwtService _jwtService;

        // UserService: Service quản lý người dùng
        private readonly UserService _userService;

        public AuthController(JwtService jwtService, UserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        // Register: Đăng ký tài khoản mới
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto request)
        {
            // Validate name and email
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Name is required.");
            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Email is required.");
            // Validate email format
            try
            {
                var addr = new System.Net.Mail.MailAddress(request.Email);
                if (addr.Address != request.Email)
                    return BadRequest("Invalid email format.");
            }
            catch
            {
                return BadRequest("Invalid email format.");
            }
            // Check duplicate email 
            if (_userService.ExistsByEmail(request.Email))
                return Conflict("Email already exists.");
            // TODO: Lưu user mới, gửi email xác thực
            _userService.CreateUser(request);
            return Ok("Registration successful. Please check your email to verify your account.");
        }

        // VerifyEmail: Xác thực tài khoản qua email (giả lập)
        [HttpGet("verify-email")]
        public IActionResult VerifyEmail(string email, string token)
        {
            // TODO: Kiểm tra token xác thực, cập nhật trạng thái user
            if (_userService.VerifyEmailToken(email, token))
                return Ok("Email verified successfully.");
            return BadRequest("Invalid email verification token.");
        }

        // ForgotPassword: Quên mật khẩu
        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("Email is required.");
                // Validate email format

                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                    return BadRequest("Invalid email format.");


                // TODO: Kiểm tra email tồn tại, gửi email đặt lại mật khẩu
                if (_userService.ExistsByEmail(email))
                    return Ok("Please check your email to reset your password.");
            }
            catch
            {
                return BadRequest("Invalid email format.");
            }
            return NotFound("Email does not exist.");
        }

        // Login: Nhận LoginRequest, xác thực và trả về LoginResponse chứa JWT token
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");
            // Check user in DB
            Models.UserDetailDTO userDetail = _userService.Authenticate(request.Username, request.Password);
            if (userDetail == null)
                return Unauthorized("Invalid username or password.");
            // Generate JWT token
            var token = _jwtService.GenerateToken(userDetail);
            return Ok(new LoginResponse
            {
                Token = token,
                UserId = userDetail.Id,
                Username = userDetail.Username,
                Role = userDetail.Role,
                ExpiresAt = _jwtService.GetTokenExpiry(token)
            });

        }
    }
}
