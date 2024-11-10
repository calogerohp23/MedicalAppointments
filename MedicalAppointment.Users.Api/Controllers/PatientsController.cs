using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Persistance.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly IPatientsRepository _patientsRepository;
        public PatientsController(IPatientsRepository patientsRepository) => _patientsRepository = patientsRepository;
        [HttpGet("GetPatients")]
        public async Task<IActionResult> Get()
        {
            var result = await _patientsRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetPatientsByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _patientsRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SavePatients")]
        public async Task<IActionResult> Post([FromBody] Patients patients)
        {
            var result = await _patientsRepository.Save(patients);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdatePatients")]
        public async Task<IActionResult> Put(int id, [FromBody] Patients patients)
        {
            var result = await _patientsRepository.Update(id, patients);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisablePatients")]
        public async Task<IActionResult> Disable(int id, [FromBody]Patients patients)
        {
            var result = await _patientsRepository.Remove(id, patients);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
