namespace WebsiteQLNhaTro.DTOs
{
    /// <summary>
    /// DTO for user response data.
    /// </summary>
    public class UserResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }
}
