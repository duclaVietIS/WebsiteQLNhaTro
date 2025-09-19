using Microsoft.AspNetCore.Mvc;
using WebsiteQLNhaTro.DTOs.roomfee;
using WebsiteQLNhaTro.Services;

namespace WebsiteQLNhaTro.Controllers
{
    [ApiController]
    [Route("api/room-fee-collection")]
    public class RoomFeeCollectionController : ControllerBase
    {
        private readonly RoomFeeCollectionService _service;
        public RoomFeeCollectionController(RoomFeeCollectionService service)
        {
            _service = service;
        }

        // GET: api/RoomFeeCollection?roomId=1&page=1&pageSize=10
        [HttpGet]
        public IActionResult GetFees([FromQuery] long roomId, [FromQuery] int page = 1, [FromQuery] int pageSize = 12)
        {
            var (fees, total) = _service.GetFees(roomId, page, pageSize).Result;
            return Ok(new { fees, total });
        }

        // GET: api/RoomFeeCollection/{id}
        [HttpGet("{id}")]
        public IActionResult GetFee(long id)
        {
            var fee = _service.GetFee(id).Result;
            if (fee == null) return NotFound();
            return Ok(fee);
        }

        // POST: api/RoomFeeCollection
        [HttpPost]
        public IActionResult CreateFee([FromForm] RoomFeeCollectionCreateDto dto)
        {
            try
            {
                // Validate required fields
                if(ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }
                IFormFile? imageElectric = Request.Form.Files["imageElectric"];
                IFormFile? imageWater = Request.Form.Files["imageWater"];
                var id = _service.CreateFee(dto, imageElectric, imageWater).Result;
                return Ok(new { id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/RoomFeeCollection/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateFee(long id, [FromForm] RoomFeeCollectionUpdateDto dto)
        {
            try
            {
                IFormFile? imageElectric = Request.Form.Files["imageElectric"];
                IFormFile? imageWater = Request.Form.Files["imageWater"];
                _service.UpdateFee(id, dto, imageElectric, imageWater).Wait();
                return Ok("Update successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/RoomFeeCollection/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteFee(long id)
        {
            _service.DeleteFee(id).Wait();
            return Ok("Delete successful. " + id);
        }
    }
}
