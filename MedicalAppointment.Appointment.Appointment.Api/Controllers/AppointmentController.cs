using MedicalAppointments.Persistance.Interfaces.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Appointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentController(IAppointmentRepository appointmentRepository) => _appointmentRepository = appointmentRepository;

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
        public async Task<IActionResult> Put([FromBody] MedicalAppointments.Domain.Entities.Appointments.Appointment appointment)
        {
            var result = await _appointmentRepository.Update(appointment);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableAppointment")]
        public async Task<IActionResult> Disable(MedicalAppointments.Domain.Entities.Appointments.Appointment appointment)
        {
            var result = await _appointmentRepository.Remove(appointment);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
