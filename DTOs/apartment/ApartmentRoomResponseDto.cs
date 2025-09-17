namespace WebsiteQLNhaTro.DTOs.apartment
{
    public class ApartmentRoomResponseDto
    {
        public long Id { get; set; }
        public long ApartmentId { get; set; }
        public required string RoomNumber { get; set; }
        public decimal DefaultPrice { get; set; }
        public long MaxTenant { get; set; }
        public int CurrentTenant { get; set; }
        public string? ImagePath { get; set; }
        public TenantInfoDto? TenantInfo { get; set; } // Thông tin người thuê
    }

    public class TenantInfoDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
