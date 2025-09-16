using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// TenantContract entity: represents a rental contract between tenant and apartment room.
    /// </summary>
    public class TenantContract
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        public long ApartmentRoomId { get; set; } // FK to ApartmentRoom

        [Required]
        public long TenantId { get; set; } // FK to Tenant

        [Required]
        public long PayPeriod { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public long ElectricityPayType { get; set; }

        [Required]
        public decimal ElectricityPrice { get; set; }

        [Required]
        public long ElectricityNumStart { get; set; }

        [Required]
        public long WaterPayType { get; set; }

        [Required]
        public decimal WaterPrice { get; set; }

        [Required]
        public long WaterNumberStart { get; set; }

        [Required]
        public long NumberOfTenantCurrent { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
