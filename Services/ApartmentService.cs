using WebsiteQLNhaTro.DTOs;
using WebsiteQLNhaTro.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebsiteQLNhaTro.Services
{
    public class ApartmentService
    {
        private readonly AppDbContext _context;

        private readonly ActionLogService _logService;

        private readonly string _imageFolder = "wwwroot/images/apartments";

        public ApartmentService(AppDbContext context, ActionLogService logService)
        {
            _context = context;
            _logService = logService;
        }

        public IEnumerable<ApartmentResponseDto> GetPagedList(int page, int pageSize, string? search)
        {
            var query = _context.Apartments.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a => a.Name.ToLower().Contains(search.ToLower()) || a.Address.ToLower().Contains(search.ToLower()));
            }
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return items.Select(a => new ApartmentResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                Address = a.Address,
                ImageUrl = a.ImagePath
            });
        }

        public async Task<ApartmentResponseDto> Create(ApartmentCreateDto dto, long userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Address))
                throw new ArgumentException("Please provide both name and address.");

            string? imagePath = null;
            string? savePath = null;

            //using transaction
            using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
            {

                try
                {
                    if (dto.Image != null)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                        savePath = Path.Combine(_imageFolder, fileName);
                        Directory.CreateDirectory(_imageFolder);
                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            dto.Image.CopyTo(stream);
                        }
                        imagePath = $"/images/apartments/{fileName}";
                    }

                    var apartment = new Apartment
                    {
                        UserId = userId,
                        Name = dto.Name,
                        Address = dto.Address,
                        ImagePath = imagePath
                    };
                    _context.Apartments.Add(apartment);
                    await _context.SaveChangesAsync();

                    // Log action
                    await _logService.LogApartmentCreated(apartment.Id, apartment.Name, userId);

                    await transaction.CommitAsync();
                    return new ApartmentResponseDto
                    {
                        Id = apartment.Id,
                        Name = apartment.Name,
                        Address = apartment.Address,
                        ImageUrl = apartment.ImagePath
                    };

                }
                catch (Exception)
                {
                    // If there is any error, rollback the transaction
                    if (savePath != null && File.Exists(savePath))
                    {
                        File.Delete(savePath);
                    }
                    await transaction.RollbackAsync();
                    throw;
                }
            }

        }

        public ApartmentResponseDto Update(long id, ApartmentUpdateDto dto)
        {
            var apartment = _context.Apartments.Find(id);
            if (apartment == null) throw new ArgumentException("Cannot find the apartment.");
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Address))
                throw new ArgumentException("Please provide both name and address.");

            apartment.Name = dto.Name;
            apartment.Address = dto.Address;
            if (dto.Image != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(dto.Image.FileName);
                var savePath = Path.Combine(_imageFolder, fileName);
                Directory.CreateDirectory(_imageFolder);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    dto.Image.CopyTo(stream);
                }
                apartment.ImagePath = $"/images/apartments/{fileName}";
            }
            _context.SaveChanges();
            return new ApartmentResponseDto
            {
                Id = apartment.Id,
                Name = apartment.Name,
                Address = apartment.Address,
                ImageUrl = apartment.ImagePath
            };
        }

        public void Delete(long id)
        {
            var apartment = _context.Apartments.Find(id);
            if (apartment == null) throw new ArgumentException("Cannot find the apartment.");
            _context.Apartments.Remove(apartment);
            _context.SaveChanges();
        }
    }
}
