using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebsiteQLNhaTro.Models;

namespace WebsiteQLNhaTro.Services
{
    public class JwtService
    {
        private readonly string _secret;
        private readonly string _issuer;

        public JwtService()
        {
            // Nên lấy từ cấu hình thực tế
            _secret = "my_super_secret_jwt_key_2025";
            _issuer = "WebsiteQLNhaTro";
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _issuer,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
