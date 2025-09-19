using System;

namespace WebsiteQLNhaTro.Entities
{
    public class UnpaidRoomStatisticsView
    {
        public long ApartmentId { get; set; }
        public string ApartmentName { get; set; }
        public long RoomId { get; set; }
        public string RoomNumber { get; set; }
        public long TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantPhone { get; set; }
        public string TenantEmail { get; set; }
        public DateTime ChargeDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal RemainingDebt { get; set; }
        public long ElectricityUsed { get; set; }
        public long WaterUsed { get; set; }

        public long ElectricityNumberBefore { get; set; }
        public long ElectricityNumberAfter { get; set; }
        public long WaterNumberBefore { get; set; }
        public long WaterNumberAfter
        {
            get; set;
        }
    }
}