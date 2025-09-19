using Microsoft.AspNetCore.Mvc;
using WebsiteQLNhaTro.Services;

namespace WebsiteQLNhaTro.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("unpaid-rooms")]
        public async Task<IActionResult> GetUnpaidRoomsStatistics()
        {
            try
            {
                var statistics = await _statisticsService.GetUnpaidRoomsStatistics();
                return Ok(statistics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("send-notifications")]
        public async Task<IActionResult> SendUnpaidNotifications()
        {
            try
            {
                await _statisticsService.SendUnpaidNotifications();
                return Ok("Notifications sent successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send notifications: {ex.Message}");
            }
        }
    }
}