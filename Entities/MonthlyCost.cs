using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// MonthlyCost entity: represents a type of monthly cost.
    /// </summary>
    public class MonthlyCost
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }
    }
}
