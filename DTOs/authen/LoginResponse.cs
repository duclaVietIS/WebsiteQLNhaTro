namespace WebsiteQLNhaTro.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }    
        public DateTime ExpiresAt { get; set; }
    }
}
