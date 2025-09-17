using System.ComponentModel.DataAnnotations;

namespace WebsiteQLNhaTro.DTOs.roomfee
{
    public class RoomFeeCollectionCreateDto
    {
        [Required]
        public long ApartmentRoomId { get; set; }

        [Required]
        public DateTime ChargeDate { get; set; }

        [Required]
        public long ElectricityNumberBefore { get; set; }

        [Required]
        public long ElectricityNumberAfter { get; set; }

        [Required]
        public long WaterNumberBefore { get; set; }

        [Required]
        public long WaterNumberAfter { get; set; }

        [Required]
        public decimal TotalDebt { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public decimal TotalPaid { get; set; }
    }
}
