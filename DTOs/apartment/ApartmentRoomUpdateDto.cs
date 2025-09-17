using System.ComponentModel.DataAnnotations;

namespace WebsiteQLNhaTro.DTOs.apartment
{
    public class ApartmentRoomUpdateDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public long ApartmentId { get; set; }

        [Required]
        [MaxLength(45)]
        public required string RoomNumber { get; set; }

        [Required]
        public decimal DefaultPrice { get; set; }

        [Required]
        public long MaxTenant { get; set; }

        public int CurrentTenant { get; set; }

        public string? ImagePath { get; set; }
    }
}
