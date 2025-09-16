namespace WebsiteQLNhaTro.DTOs
{
    public class ApartmentCreateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public IFormFile? Image { get; set; } // optional
    }
}
