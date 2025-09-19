using System.ComponentModel.DataAnnotations;

namespace WebsiteQLNhaTro.DTOs.roomfee
{
    public class RoomFeeCollectionCreateDto : IValidatableObject
    {
        // [Required(ErrorMessage = "ApartmentRoomId is required")]
        // [Range(1, long.MaxValue, ErrorMessage = "ApartmentRoomId must be greater than 0")]
        public long ApartmentRoomId { get; set; }

        [Required(ErrorMessage = "TenantContractId is required")]
        public long TenantContractId { get; set; }

        [Required(ErrorMessage = "TenantId is required")]
        public long TenantId { get; set; }


        [Required(ErrorMessage = "ChargeDate is required")]
        [DataType(DataType.Date)]
        public DateTime ChargeDate { get; set; }

        [Required(ErrorMessage = "Electricity number before is required")]
        [Range(0, long.MaxValue, ErrorMessage = "Electricity number before must be non-negative")]
        public long? ElectricityNumberBefore { get; set; }

        [Required(ErrorMessage = "Electricity number after is required")]
        [Range(0, long.MaxValue, ErrorMessage = "Electricity number after must be non-negative")]
        public long? ElectricityNumberAfter { get; set; }

        [Required(ErrorMessage = "Water number before is required")]
        [Range(0, long.MaxValue, ErrorMessage = "Water number before must be non-negative")]
        public long? WaterNumberBefore { get; set; }

        [Required(ErrorMessage = "Water number after is required")]
        [Range(0, long.MaxValue, ErrorMessage = "Water number after must be non-negative")]
        public long? WaterNumberAfter { get; set; }

        [Required(ErrorMessage = "Total debt is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total debt must be non-negative")]
        public decimal? TotalDebt { get; set; }

        [Required(ErrorMessage = "Total price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be non-negative")]
        public decimal? TotalPrice { get; set; }

        [Required(ErrorMessage = "Total paid is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Total paid must be non-negative")]
        public decimal? TotalPaid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ElectricityNumberAfter < ElectricityNumberBefore)
            {
                yield return new ValidationResult(
                    "Electricity number after must be greater than electricity number before",
                    new[] { nameof(ElectricityNumberAfter) });
            }

            if (WaterNumberAfter < WaterNumberBefore)
            {
                yield return new ValidationResult(
                    "Water number after must be greater than water number before",
                    new[] { nameof(WaterNumberAfter) });
            }

            if (ChargeDate > DateTime.Now)
            {
                yield return new ValidationResult(
                    "Charge date cannot be in the future",
                    new[] { nameof(ChargeDate) });
            }
        }
    }
}
