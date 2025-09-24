using Microsoft.EntityFrameworkCore;
using WebsiteQLNhaTro.DTOs.statistics;
using WebsiteQLNhaTro.Entities;

namespace WebsiteQLNhaTro.Services
{
    public class StatisticsService
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;
        private readonly ActionLogService _logService;

        public StatisticsService(AppDbContext context, EmailService emailService, ActionLogService logService)
        {
            _context = context;
            _emailService = emailService;
            _logService = logService;
        }

        /// <summary>
        /// Get unpaid room statistics
        /// </summary>
        /// <param name="apartmentId">Optional apartment ID to filter results</param>
        /// <returns>Unpaid room statistics response</returns>
        public async Task<UnpaidRoomStatisticsResponse> GetUnpaidRoomsStatistics(long? apartmentId = null)
        {
            // Call the stored procedure to get unpaid room statistics
            var unpaidRooms = await _context.Database
                .SqlQuery<UnpaidRoomStatisticsDto>($"CALL sp_get_unpaid_room_statistics({(apartmentId.HasValue ? apartmentId.Value.ToString() : "NULL")});")
                .ToListAsync();

            return new UnpaidRoomStatisticsResponse
            {
                TotalUnpaidRooms = unpaidRooms.Count,
                TotalUnpaidAmount = unpaidRooms.Sum(r => r.RemainingDebt),
                UnpaidRooms = unpaidRooms
            };
        }

        // TODO : implement sending email notifications to tenants with unpaid rooms
        public async Task SendUnpaidNotifications(long? apartmentId = null)
        {
            var statistics = await GetUnpaidRoomsStatistics(apartmentId);

            foreach (var room in statistics.UnpaidRooms)
            {
                var tenant = await _context.Tenants
                    .FirstOrDefaultAsync(t => t.Id == room.TenantId);

                if (tenant?.Email != null)
                {
                    await _emailService.SendUnpaidRoomNotification(tenant.Email, room);
                }
            }

            // Log the notification run summary
            await _logService.LogUnpaidNotificationRun(statistics.TotalUnpaidRooms, statistics.TotalUnpaidAmount, apartmentId);
        }
    }
}