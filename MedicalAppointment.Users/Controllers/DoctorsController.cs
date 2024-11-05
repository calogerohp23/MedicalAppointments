using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Persistance.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsRepository _doctorsRepository;
        public DoctorsController(IDoctorsRepository doctorsRepository) => _doctorsRepository = doctorsRepository;
        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var result = await _doctorsRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetDoctorsByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _doctorsRepository.GetEntityBy(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveDoctors")]
        public async Task<IActionResult> Post([FromBody] Doctors doctors)
        {
            var result = await _doctorsRepository.Save(doctors);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateDoctors")]
        public async Task<IActionResult> Put([FromBody] Doctors doctors)
        {
            var result = await _doctorsRepository.Update(doctors);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableDoctors")]
        public async Task<IActionResult> Delete(Doctors doctors)
        {
            var result = await _doctorsRepository.Remove(doctors);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
