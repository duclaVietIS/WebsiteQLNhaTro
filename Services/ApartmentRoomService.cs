using WebsiteQLNhaTro.DTOs.apartment;
using WebsiteQLNhaTro.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebsiteQLNhaTro.Services
{
    public class ApartmentRoomService
    {
        private readonly AppDbContext _db; 
        private readonly IWebHostEnvironment _env;
        public ApartmentRoomService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<(List<ApartmentRoomResponseDto>, int)> GetRooms(long apartmentId, int page, int pageSize, string? search)
        {
            var query = _db.ApartmentRooms.Where(r => r.ApartmentId == apartmentId);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(r => r.RoomNumber.Contains(search));
            int total = await query.CountAsync();
            var rooms = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var result = rooms.Select(r => new ApartmentRoomResponseDto
            {
                Id = r.Id,
                ApartmentId = r.ApartmentId,
                RoomNumber = r.RoomNumber,
                DefaultPrice = r.DefaultPrice,
                MaxTenant = r.MaxTenant,
            }).ToList();
            return (result, total);
        }

        public async Task<ApartmentRoomResponseDto?> GetRoom(long id)
        {
            // Find room by ID
            var r = await _db.ApartmentRooms.FindAsync(id);
            if (r == null) return null;
            var roomDto = new ApartmentRoomResponseDto
            {
                Id = r.Id,
                ApartmentId = r.ApartmentId,
                RoomNumber = r.RoomNumber,
                DefaultPrice = r.DefaultPrice,
                MaxTenant = r.MaxTenant,
            };

            // Find latest contract of the room
            var latestContract = _db.TenantContracts
                .Where(tc => tc.ApartmentRoomId == r.Id)
                .OrderByDescending(tc => tc.EndDate ?? DateTime.MaxValue)
                .FirstOrDefault();
            if (latestContract != null && latestContract.EndDate > DateTime.Now)
            {
                // Find tenant info
                var tenantInfo = await _db.Tenants.FindAsync(latestContract.TenantId);
                roomDto.TenantInfo = new TenantInfoDto
                {
                    Name = tenantInfo?.Name,
                    Phone = tenantInfo?.Tel,
                    Email = tenantInfo?.Email
                };                 
            }
            return roomDto;
        }

        public async Task<long> CreateRoom(ApartmentRoomCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.RoomNumber))
                throw new Exception("Room number is required.");
            var room = new ApartmentRoom
            {
                ApartmentId = dto.ApartmentId,
                RoomNumber = dto.RoomNumber,
                DefaultPrice = dto.DefaultPrice,
                MaxTenant = dto.MaxTenant
            };
            _db.ApartmentRooms.Add(room);
            await _db.SaveChangesAsync();
            return room.Id;
        }

        public async Task UpdateRoom(long id, ApartmentRoomUpdateDto dto)
        {
            var room = await _db.ApartmentRooms.FindAsync(id);
            if (room == null)
                throw new Exception("Cannot find the room.");
            if (string.IsNullOrWhiteSpace(dto.RoomNumber))
                throw new Exception("Room number is required 1xx.");
            room.RoomNumber = dto.RoomNumber;
            room.DefaultPrice = dto.DefaultPrice;
            room.MaxTenant = dto.MaxTenant;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            var room = await _db.ApartmentRooms.FindAsync(id);
            if (room == null) throw new Exception("Cannot find the room.");
            _db.ApartmentRooms.Remove(room);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTenant(long id, TenantInfoDto tenant)
        {
            var room = await _db.ApartmentRooms.FindAsync(id);
            if (room == null) throw new Exception("Cannot find the room.");
            // Update tenant info
            var tenantContract = await _db.TenantContracts.FirstOrDefaultAsync(t => t.ApartmentRoomId == room.Id);
            if (tenantContract == null)
            {
                throw new Exception("No active tenant contract found for this room.");
            }
            var tenantEntity = await _db.Tenants.FindAsync(tenantContract.TenantId);
            if (tenantEntity == null)
            {
                throw new Exception("Tenant not found.");
            }
            tenantEntity.Name = tenant.Name ?? tenantEntity.Name;
            tenantEntity.Tel = tenant.Phone ?? tenantEntity.Tel;
            tenantEntity.Email = tenant.Email ?? tenantEntity.Email;
            await _db.SaveChangesAsync();
        }
    }
}
