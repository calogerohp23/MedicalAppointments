using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Persistance.Interfaces.Appointment;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.Appointment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController(IDoctorAvailabilityRepository doctorAvailabilityRepository) : ControllerBase
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository = doctorAvailabilityRepository;

        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorAvailabilityRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetDoctorAvailabilityByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorAvailabilityRepository.GetEntityBy(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveDoctorAvailability")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityRepository.Save(doctorAvailability);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateDoctorAvailability")]
        public async Task<IActionResult> Put(int id, [FromBody] DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityRepository.Update(id, doctorAvailability);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableDoctorAvailability")]
        public async Task<IActionResult> Delete(int id, DoctorAvailability doctorAvailability)
        {
            var result = await _doctorAvailabilityRepository.Remove(id, doctorAvailability);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
