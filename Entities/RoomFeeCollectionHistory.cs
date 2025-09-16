using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// RoomFeeCollectionHistory entity: represents payment history for a room fee collection.
    /// </summary>
    public class RoomFeeCollectionHistory
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        public long RoomFeeCollectionId { get; set; } // FK to RoomFeeCollection

        [Required]
        public DateTime PaidDate { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
