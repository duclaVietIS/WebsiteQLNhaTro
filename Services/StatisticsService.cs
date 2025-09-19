using Microsoft.EntityFrameworkCore;
using WebsiteQLNhaTro.DTOs.statistics;
using WebsiteQLNhaTro.Entities;

namespace WebsiteQLNhaTro.Services
{
    public class StatisticsService
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public StatisticsService(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<UnpaidRoomStatisticsResponse> GetUnpaidRoomsStatistics()
        {
            var unpaidRooms = await _context.UnpaidRoomStatistics
                .Select(v => new UnpaidRoomStatisticsDto
                {
                    RoomId = v.RoomId,
                    RoomNumber = v.RoomNumber,
                    TenantId = v.TenantId,
                    TenantName = v.TenantName,
                    Tel = v.TenantPhone,
                    Email = v.TenantEmail,
                    ChargeDate = v.ChargeDate,
                    TotalPrice = v.TotalPrice,
                    TotalPaid = v.TotalPaid,
                    RemainingDebt = v.TotalPrice - (v.TotalPaid ?? 0),
                    ElectricityUsed = v.ElectricityNumberAfter - v.ElectricityNumberBefore,
                    WaterUsed = v.WaterNumberAfter - v.WaterNumberBefore
                })
                .ToListAsync();

            return new UnpaidRoomStatisticsResponse
            {
                TotalUnpaidRooms = unpaidRooms.Count,
                TotalUnpaidAmount = unpaidRooms.Sum(r => r.RemainingDebt),
                UnpaidRooms = unpaidRooms
            };
        }

        public async Task SendUnpaidNotifications()
        {
            var statistics = await GetUnpaidRoomsStatistics();

            foreach (var room in statistics.UnpaidRooms)
            {
                var tenant = await _context.Tenants
                    .FirstOrDefaultAsync(t => t.Id == room.TenantId);

                if (tenant?.Email != null)
                {
                    await _emailService.SendUnpaidRoomNotification(tenant.Email, room);
                }
            }
        }
    }
}