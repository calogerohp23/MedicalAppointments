using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Persistance.Interfaces.Medical;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController(ISpecialtiesRepository specialtiesRepository) : ControllerBase
    {
        private readonly ISpecialtiesRepository _specialtiesRepository = specialtiesRepository;
        [HttpGet("GetSpecialties")]
        public async Task<IActionResult> Get()
        {
            var result = await _specialtiesRepository.GetAll();
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetSpecialtiesByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _specialtiesRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveSpecialties")]
        public async Task<IActionResult> Post([FromBody] Specialties specialties)
        {
            var result = await _specialtiesRepository.Save(specialties);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateSpecialties")]
        public async Task<IActionResult> Put(int id, [FromBody] Specialties specialties)
        {
            var result = await _specialtiesRepository.Update(id, specialties);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableSpecialties")]
        public async Task<IActionResult> Delete(int id, [FromBody] Specialties specialties)
        {
            var result = await _specialtiesRepository.Remove(id, specialties);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
