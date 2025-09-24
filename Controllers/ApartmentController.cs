using Microsoft.AspNetCore.Mvc;
using WebsiteQLNhaTro.DTOs;
using WebsiteQLNhaTro.Entities;
using WebsiteQLNhaTro.Services;
using System.Security.Claims;

namespace WebsiteQLNhaTro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly ApartmentService _apartmentService;
        public ApartmentController(ApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        // GET: api/apartment?page=1&pageSize=10&search=abc
        [HttpGet]
        public IActionResult GetAll(int page = 1, int pageSize = 10, string? search = null)
        {
            var result = _apartmentService.GetPagedList(page, pageSize, search);
            return Ok(result);
        }

        // POST: api/apartment
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ApartmentCreateDto dto)
        {
            // get id user from token
            var userIdStr = User.FindFirstValue("user_id");
            if (userIdStr == null)
                return Unauthorized("User ID not found in token.");
            if (!long.TryParse(userIdStr, out var userId))
                return BadRequest("Invalid user ID in token.");
            var result = await _apartmentService.Create(dto, userId);
            return Ok(result);
        }

        // PUT: api/apartment/{id}
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromForm] ApartmentUpdateDto dto)
        {
            var result = _apartmentService.Update(id, dto);
            return Ok(result);
        }

        // DELETE: api/apartment/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _apartmentService.Delete(id);
            return Ok();
        }
    }
}
