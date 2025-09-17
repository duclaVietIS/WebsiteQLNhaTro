using WebsiteQLNhaTro.DTOs.roomfee;
using WebsiteQLNhaTro.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebsiteQLNhaTro.Services
{
    public class RoomFeeCollectionService
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public RoomFeeCollectionService(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<(List<RoomFeeCollectionResponseDto>, int)> GetFees(long roomId, int page, int pageSize)
        {
            var query = _db.RoomFeeCollections.Where(f => f.ApartmentRoomId == roomId);
            int total = await query.CountAsync();
            var fees = await query.OrderByDescending(f => f.ChargeDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
            var result = fees.Select(f => new RoomFeeCollectionResponseDto
            {
                Id = f.Id,
                ApartmentRoomId = f.ApartmentRoomId,
                ChargeDate = f.ChargeDate,
                ElectricityNumberBefore = f.ElectricityNumberBefore,
                ElectricityNumberAfter = f.ElectricityNumberAfter,
                WaterNumberBefore = f.WaterNumberBefore,
                WaterNumberAfter = f.WaterNumberAfter,
                TotalDebt = f.TotalDebt,
                TotalPrice = f.TotalPrice,
                TotalPaid = f.TotalPaid,
                ImageElectricPath = f.ImageElectricPath,
                ImageWaterPath = f.ImageWaterPath
            }).ToList();
            return (result, total);
        }

        public async Task<RoomFeeCollectionResponseDto?> GetFee(long id)
        {
            var f = await _db.RoomFeeCollections.FindAsync(id);
            if (f == null) return null;
            return new RoomFeeCollectionResponseDto
            {
                Id = f.Id,
                ApartmentRoomId = f.ApartmentRoomId,
                ChargeDate = f.ChargeDate,
                ElectricityNumberBefore = f.ElectricityNumberBefore,
                ElectricityNumberAfter = f.ElectricityNumberAfter,
                WaterNumberBefore = f.WaterNumberBefore,
                WaterNumberAfter = f.WaterNumberAfter,
                TotalDebt = f.TotalDebt,
                TotalPrice = f.TotalPrice,
                TotalPaid = f.TotalPaid,
                ImageElectricPath = f.ImageElectricPath,
                ImageWaterPath = f.ImageWaterPath
            };
        }

        public async Task<long> CreateFee(RoomFeeCollectionCreateDto dto, IFormFile? imageElectric, IFormFile? imageWater)
        {
            string? imageElectricPath = null;
            string? imageWaterPath = null;
            if (imageElectric != null)
            {
                var fileName = $"electric_{Guid.NewGuid()}{Path.GetExtension(imageElectric.FileName)}";
                var savePath = Path.Combine(_env.WebRootPath, "images", "meters", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                using var stream = new FileStream(savePath, FileMode.Create);
                await imageElectric.CopyToAsync(stream);
                imageElectricPath = $"/images/meters/{fileName}";
            }
            if (imageWater != null)
            {
                var fileName = $"water_{Guid.NewGuid()}{Path.GetExtension(imageWater.FileName)}";
                var savePath = Path.Combine(_env.WebRootPath, "images", "meters", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
                using var stream = new FileStream(savePath, FileMode.Create);
                await imageWater.CopyToAsync(stream);
                imageWaterPath = $"/images/meters/{fileName}";
            }
            
            var fee = new RoomFeeCollection
            {
                ApartmentRoomId = dto.ApartmentRoomId,
                ChargeDate = dto.ChargeDate,
                ElectricityNumberBefore = dto.ElectricityNumberBefore,
                ElectricityNumberAfter = dto.ElectricityNumberAfter,
                WaterNumberBefore = dto.WaterNumberBefore,
                WaterNumberAfter = dto.WaterNumberAfter,
                TotalDebt = dto.TotalDebt,
                TotalPrice = dto.TotalPrice,
                TotalPaid = dto.TotalPaid,
                ImageElectricPath = imageElectricPath,
                ImageWaterPath = imageWaterPath
            };
            _db.RoomFeeCollections.Add(fee);
            await _db.SaveChangesAsync();
            return fee.Id;
        }

        public async Task UpdateFee(long id, RoomFeeCollectionUpdateDto dto, IFormFile? imageElectric, IFormFile? imageWater)
        {
            var fee = await _db.RoomFeeCollections.FindAsync(id);
            if (fee == null)
                throw new Exception("Cannot find fee information");
            // Check required fields is not null
            if (dto.ElectricityNumberAfter == null || dto.WaterNumberAfter == null || dto.TotalPaid == null)
                throw new Exception("Number of electricity, number of water, and total paid are required");
            fee.ChargeDate = dto.ChargeDate;
            fee.ElectricityNumberBefore = dto.ElectricityNumberBefore;
            fee.ElectricityNumberAfter = dto.ElectricityNumberAfter;
            fee.WaterNumberBefore = dto.WaterNumberBefore;
            fee.WaterNumberAfter = dto.WaterNumberAfter;
            fee.TotalDebt = dto.TotalDebt;
            fee.TotalPrice = dto.TotalPrice;
            fee.TotalPaid = dto.TotalPaid;
            if (imageElectric != null)
            {
                var fileName = $"electric_{Guid.NewGuid()}{Path.GetExtension(imageElectric.FileName)}";
                using FileStream stream = await saveImage(imageElectric, fileName);
                fee.ImageElectricPath = $"/images/meters/{fileName}";
            }
            if (imageWater != null)
            {
                var fileName = $"water_{Guid.NewGuid()}{Path.GetExtension(imageWater.FileName)}";
                using FileStream stream = await saveImage(imageWater, fileName);
                fee.ImageWaterPath = $"/images/meters/{fileName}";
            }
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// Save image file and return filestream
        /// </summary>
        /// <param name="imageWater"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task<FileStream> saveImage(IFormFile imageWater, string fileName)
        {
            var savePath = Path.Combine(_env.WebRootPath, "images", "meters", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
            var stream = new FileStream(savePath, FileMode.Create);
            await imageWater.CopyToAsync(stream);
            return stream;
        }

        public async Task DeleteFee(long id)
        {
            var fee = await _db.RoomFeeCollections.FindAsync(id);
            if (fee == null)
                throw new Exception("Cannot find fee information");
            _db.RoomFeeCollections.Remove(fee);
            await _db.SaveChangesAsync();
        }
    }
}
