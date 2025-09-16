using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// Admin entity: represents an administrator of the system.
    /// </summary>
    public class Admin
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        [MaxLength(64)]
        public string AdminUuid { get; set; }

        [Required]
        [MaxLength(64)]
        public string AdminLoginId { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        [MaxLength(32)]
        public string Role { get; set; } // e.g. "super_admin", "admin"
    }
}
