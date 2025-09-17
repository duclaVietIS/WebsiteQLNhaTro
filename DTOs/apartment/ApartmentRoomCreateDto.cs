using System.ComponentModel.DataAnnotations;

namespace WebsiteQLNhaTro.DTOs.apartment
{
    public class ApartmentRoomCreateDto
    {
        [Required]
        public long ApartmentId { get; set; }

        [Required]
        [MaxLength(45)]
        public string RoomNumber { get; set; }

        [Required]
        public decimal DefaultPrice { get; set; }

        [Required]
        public long MaxTenant { get; set; }

        public int CurrentTenant { get; set; } // Số người đang ở

        public string? ImagePath { get; set; } // Ảnh phòng trọ (optional)
    }
}
