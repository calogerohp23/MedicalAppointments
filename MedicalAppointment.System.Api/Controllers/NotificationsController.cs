using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Persistance.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationsRepository _notificationRepository;
        public NotificationsController(INotificationsRepository notificationRepository) => _notificationRepository = notificationRepository;
        [HttpGet("GetNotifications")]
        public async Task<IActionResult> Get()
        {
            var result = await _notificationRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetNotificationById")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _notificationRepository.GetEntityBy(id);
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveNotification")]
        public async Task<IActionResult> Post([FromBody] Notifications notifications)
        {
            var result = await _notificationRepository.Save(notifications);
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateNotfication")]
        public async Task<IActionResult> Put([FromBody] Notifications notifications)
        {
            var result = await _notificationRepository.Update(notifications);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableNotification")]
        public async Task<IActionResult> Disable(Notifications notifications)
        {
            var result = await _notificationRepository.Remove(notifications);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
