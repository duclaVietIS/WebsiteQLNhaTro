using System.ComponentModel.DataAnnotations;


namespace WebsiteQLNhaTro.Entities
{
    /// <summary>
    /// ContractMonthlyCost entity: represents monthly cost for a contract.
    /// </summary>
    public class ContractMonthlyCost
    {
        [Key]
        public long Id { get; set; } // PK

        [Required]
        public long TenantContractId { get; set; } // FK to TenantContract

        [Required]
        public long MonthlyCostId { get; set; } // FK to MonthlyCost

        [Required]
        public long PayType { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
