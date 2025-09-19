using System;

namespace WebsiteQLNhaTro.DTOs.statistics
{
    // DTO cho thống kê phòng chưa thanh toán đủ
    public class UnpaidRoomStatisticsDto
    {
        public long ApartmentId { get; set; }
        public required string ApartmentName { get; set; }
        public long RoomId { get; set; }
        public required string RoomNumber { get; set; }
        public long TenantId { get; set; }
        public required string TenantName { get; set; }
        public required string TenantPhone { get; set; }
        public required string TenantEmail { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal RemainingDebt { get; set; }
        public long ElectricityUsed { get; set; }
        public long WaterUsed { get; set; }

        // Các trường bổ sung nếu cần/...
        public long ElectricityNumberBefore { get; set; }
        public long ElectricityNumberAfter { get; set; }
        public long WaterNumberBefore { get; set; }
        public long WaterNumberAfter { get; set; }

    }

    // DTO cho phản hồi thống kê phòng chưa thanh toán đủ
    public class UnpaidRoomStatisticsResponse
    {
        public int TotalUnpaidRooms { get; set; }
        public decimal TotalUnpaidAmount { get; set; }
        public required List<UnpaidRoomStatisticsDto> UnpaidRooms { get; set; }
    }
}