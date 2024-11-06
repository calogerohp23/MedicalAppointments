using MedicalAppointments.Persistance.Interfaces.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Appointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController(IAppointmentRepository appointmentRepository) : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;

        [HttpGet("GetAppointments")]
        public async Task<IActionResult> Get()
        {
            var result = await _appointmentRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetAppointmentByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _appointmentRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveAppointment")]
        public async Task<IActionResult> Post([FromBody] MedicalAppointments.Domain.Entities.Appointments.Appointment appointment)
        {
            var result = await _appointmentRepository.Save(appointment);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        
        [HttpPut("UpdateAppointment")]
        public async Task<IActionResult> Put(int id, [FromBody] MedicalAppointments.Domain.Entities.Appointments.Appointment appointment)
        {
            var result = await _appointmentRepository.Update(id,appointment);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableAppointment")]
        public async Task<IActionResult> Disable(int id, MedicalAppointments.Domain.Entities.Appointments.Appointment appointment)
        {
            var result = await _appointmentRepository.Remove(id, appointment);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
