using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// ApartmentRoom entity: represents a room in an apartment.
    /// </summary>
    public class ApartmentRoom
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        public long ApartmentId { get; set; } // FK to Apartment

        [Required]
        [MaxLength(45)]
        public string RoomNumber { get; set; }

        [Required]
        public decimal DefaultPrice { get; set; }

        [Required]
        public long MaxTenant { get; set; }
    }
}
