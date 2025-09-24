using System.ComponentModel.DataAnnotations;

namespace WebsiteQLNhaTro.Entities
{
    public class ActionLog
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(128)]
        public string Action { get; set; } = string.Empty;

        public string? Description { get; set; }

        public long? UserId { get; set; }
        public long? ApartmentId { get; set; }

        public long? RoomId { get; set; }

        public bool Success { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
