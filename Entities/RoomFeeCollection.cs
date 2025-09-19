using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// RoomFeeCollection entity: represents a fee collection for a room contract.
    /// </summary>
    public class RoomFeeCollection
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        public long TenantContractId { get; set; } // FK to TenantContract

        [Required]
        public long ApartmentRoomId { get; set; } // FK to ApartmentRoom

        [Required]
        public long TenantId { get; set; } // FK to Tenant

        [Required]
        public long? ElectricityNumberBefore { get; set; }

        [Required]
        public long? ElectricityNumberAfter { get; set; }

        [Required]
        public long? WaterNumberBefore { get; set; }

        [Required]
        public long? WaterNumberAfter { get; set; }

        [Required]
        public DateTime ChargeDate { get; set; }

        [Required]
        public decimal? TotalDebt { get; set; }

        [Required]
        public decimal? TotalPrice { get; set; }

        [Required]
        public decimal? TotalPaid { get; set; }

        // [MaxLength(64)]
        // public string FeeCollectionUuid { get; set; }

        [MaxLength(256)]
        public string? ImageElectricPath { get; set; }

        [MaxLength(256)]
        public string? ImageWaterPath { get; set; }
    }
}
