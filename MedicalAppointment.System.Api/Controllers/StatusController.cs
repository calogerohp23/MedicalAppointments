using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Persistance.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;
        public StatusController(IStatusRepository statusRepository) => _statusRepository = statusRepository;
        [HttpGet("GetStatus")]
        public async Task<IActionResult> Get()
        {
            var result = await _statusRepository.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetStatusByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _statusRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("SaveStatus")]
        public async Task<IActionResult> Post([FromBody] Status status)
        {
            var result = await _statusRepository.Save(status);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> Put(int id, [FromBody] Status status)
        {
            var result = await _statusRepository.Update(id, status);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("DisableStatus")]
        public async Task<IActionResult> Disable(int id, [FromBody] Status status)
        {
            var result = await _statusRepository.Remove(id, status);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
