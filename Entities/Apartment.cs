using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// Apartment entity: represents a rental apartment.
    /// </summary>
    public class Apartment
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        public long UserId { get; set; } // FK to User

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string ProvinceId { get; set; }

        [MaxLength(256)]
        public string DistrictId { get; set; }

        [MaxLength(256)]
        public string WardId { get; set; }

        [MaxLength(256)]
        public string Address { get; set; }

        [MaxLength(256)]
        public string? ImagePath { get; set; }
    }
}
