using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// ElectricityUsage entity: represents electricity usage for a room.
    /// </summary>
    public class ElectricityUsage
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        public long ApartmentRoomId { get; set; } // FK to ApartmentRoom

        [Required]
        public long UsageNumber { get; set; }

        [Required]
        public DateTime InputDate { get; set; }
    }
}
