using WebsiteQLNhaTro.Entities;

namespace WebsiteQLNhaTro.Services
{
    public class ActionLogService
    {
        private readonly AppDbContext _db;

        public ActionLogService(AppDbContext db)
        {
            _db = db;
        }

        public async Task LogAsync(string action,
                                    string? description = null,
                                    long? userId = null,
                                    long? apartmentId = null,
                                    long? roomId = null,
                                    bool success = true)
        {
            var log = new ActionLog
            {
                Action = action,
                Description = description,
                UserId = userId,
                ApartmentId = apartmentId,
                RoomId = roomId,
                Success = success,
                CreatedAt = DateTime.UtcNow
            };
            _db.ActionLogs.Add(log);
            await _db.SaveChangesAsync();
        }

        public Task LogApartmentCreated(long apartmentId, string name, long? userId = null)
            => LogAsync("APARTMENT_CREATED", $"Created apartment '{name}' (ID={apartmentId})", userId, apartmentId, null, true);

        public Task LogRoomCreated(long roomId, long apartmentId, string roomNumber, long? userId = null)
            => LogAsync("ROOM_CREATED", $"Created room '{roomNumber}' (ID={roomId}) for apartment {apartmentId}", userId, apartmentId, roomId, true);

        public Task LogUnpaidNotificationRun(int totalRooms, decimal totalAmount, long? apartmentId = null)
            => LogAsync("UNPAID_NOTIFICATION_RUN", $"Sent notifications for {totalRooms} rooms, total unpaid {totalAmount:N0}", null, apartmentId, null, true);
    }
}
