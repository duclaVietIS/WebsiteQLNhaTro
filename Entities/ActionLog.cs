using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteQLNhaTro.Entities
{
    [Table("action_logs")]
    public class ActionLog
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("action")]
        [MaxLength(128)]
        public string Action { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Column("user_id")]
        public long? UserId { get; set; }

        [Column("apartment_id")]
        public long? ApartmentId { get; set; }

        [Column("room_id")]
        public long? RoomId { get; set; }

        [Column("success")]
        public bool Success { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
