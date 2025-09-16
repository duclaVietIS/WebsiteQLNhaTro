using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// Tenant entity: represents a person renting a room.
    /// </summary>
    public class Tenant
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(45)]
        public string Tel { get; set; }

        [MaxLength(45)]
        public string IdentityCardNumber { get; set; }

        [MaxLength(256)]
        public string Email { get; set; }
    }
}
