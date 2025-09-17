using Microsoft.AspNetCore.Mvc;
using WebsiteQLNhaTro.DTOs.apartment;
using WebsiteQLNhaTro.Entities;
using System.ComponentModel.DataAnnotations;
using WebsiteQLNhaTro.Services;

namespace WebsiteQLNhaTro.Controllers
{
    [ApiController]
    [Route("api/apartment-room")]
    public class ApartmentRoomController : ControllerBase
    {
        private readonly ApartmentRoomService _service;
        public ApartmentRoomController(ApartmentRoomService service)
        {
            _service = service;
        }

        // GET: api/ApartmentRoom?apartmentId=1&page=1&pageSize=10&search=101
        [HttpGet]
        public IActionResult GetRooms([FromQuery] long apartmentId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
        {
            (List<ApartmentRoomResponseDto> rooms, int total) = _service.GetRooms(apartmentId, page, pageSize, search).Result;
            return Ok(new { rooms, total });
        }

        // GET: api/ApartmentRoom/{id}
        [HttpGet("{id}")]
        public IActionResult GetRoom(long id)
        {
            var room = _service.GetRoom(id).Result;
            if (room == null) return NotFound();
            return Ok(room);
        }

        // POST: api/ApartmentRoom
        [HttpPost]
        public IActionResult CreateRoom([FromBody] ApartmentRoomCreateDto dto)
        {
            var id = _service.CreateRoom(dto).Result;
            return Ok(new { id });
        }

        // PUT: api/ApartmentRoom/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(long id, [FromBody] ApartmentRoomUpdateDto dto)
        {
            try
            {
                _service.UpdateRoom(id, dto).Wait();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Update successful.");
        }

        // DELETE: api/ApartmentRoom/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(long id)
        {
            _service.DeleteRoom(id).Wait();
            return Ok();
        }

        // PUT: api/ApartmentRoom/{id}/tenant
        [HttpPut("{id}/tenant")]
        public IActionResult UpdateTenant(long id, [FromBody] TenantInfoDto tenant)
        {
            _service.UpdateTenant(id, tenant).Wait();
            return Ok();
        }
    }
}
