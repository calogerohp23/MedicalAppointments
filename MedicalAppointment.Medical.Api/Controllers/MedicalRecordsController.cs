using MedicalAppointments.Domain.Entities.Medical;
using MedicalAppointments.Persistance.Interfaces.Medical;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Medical.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController(IMedicalRecordsRepository medicalRecordsRepository) : ControllerBase
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository = medicalRecordsRepository;
        [HttpGet("GetMedicalRecords")]
        public async Task<IActionResult> Get()
        {
            var result = await _medicalRecordsRepository.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetMedicalRecordsByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _medicalRecordsRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveMedicalRecords")]
        public async Task<IActionResult> Post([FromBody] MedicalRecords medicalRecords)
        {
            var result = await _medicalRecordsRepository.Save(medicalRecords);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateMedicalRecords")]
        public async Task<IActionResult> Put(int id, [FromBody] MedicalRecords medicalRecords)
        {
            var result = await _medicalRecordsRepository.Update(id, medicalRecords);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableMedicalRecords")]
        public async Task<IActionResult> Delete(int id, [FromBody] MedicalRecords medicalRecords)
        {
            var result = await _medicalRecordsRepository.Remove(id, medicalRecords);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
