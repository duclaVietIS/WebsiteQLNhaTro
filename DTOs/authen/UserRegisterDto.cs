namespace WebsiteQLNhaTro.DTOs
{
    /// <summary>
    /// DTO for user registration.
    /// </summary>
    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
