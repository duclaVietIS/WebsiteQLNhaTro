namespace WebsiteQLNhaTro.DTOs.roomfee
{
    public class RoomFeeCollectionResponseDto
    {
        public long Id { get; set; }
        public long ApartmentRoomId { get; set; }
        public DateTime ChargeDate { get; set; }
        public long? ElectricityNumberBefore { get; set; }
        public long? ElectricityNumberAfter { get; set; }
        public long? WaterNumberBefore { get; set; }
        public long? WaterNumberAfter { get; set; }
        public decimal? TotalDebt { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalPaid { get; set; }
        public string ImageElectricPath { get; set; }
        public string ImageWaterPath { get; set; }
    }
}
