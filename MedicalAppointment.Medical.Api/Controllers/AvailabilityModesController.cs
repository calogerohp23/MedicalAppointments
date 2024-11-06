using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Persistance.Interfaces.Medical;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityModesController(IAvailabilityModesRepository availabilityModesRepository) : ControllerBase
    {
        private readonly IAvailabilityModesRepository _availabilityModesRepository = availabilityModesRepository;
        [HttpGet("GetAvailabilityModes")]
        public async Task<IActionResult> Get()
        {
            var result = await _availabilityModesRepository.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetAvailabilityModesByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _availabilityModesRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveAvailabilityModes")]
        public async Task<IActionResult> Post([FromBody] AvailabilityModes availabilityModes)
        {
            var result = await _availabilityModesRepository.Save(availabilityModes);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateAvailabilityModes")]
        public async Task<IActionResult> Put(int id, [FromBody] AvailabilityModes availabilityModes)
        {
            var result = await _availabilityModesRepository.Update(id, availabilityModes);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableAvailabilityModes")]
        public async Task<IActionResult> Delete(int id, [FromBody] AvailabilityModes availabilityModes)
        {
            var result = await _availabilityModesRepository.Remove(id, availabilityModes);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
