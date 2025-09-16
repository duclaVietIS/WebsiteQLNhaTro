using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // e.g. Admin, Owner, Tenant

        public bool IsActive { get; set; } = true;
    }
}
