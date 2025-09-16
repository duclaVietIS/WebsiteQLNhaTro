namespace WebsiteQLNhaTro.DTOs
{
    public class ApartmentUpdateDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public IFormFile? Image { get; set; } // optional
    }
}
