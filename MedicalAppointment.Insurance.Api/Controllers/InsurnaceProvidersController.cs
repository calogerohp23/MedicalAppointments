using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProvidersController(IInsuranceProvidersRepository insuranceProvidersRepository) : ControllerBase
    {
        private readonly IInsuranceProvidersRepository _insuranceProvidersRepository = insuranceProvidersRepository;

        [HttpGet("GetInsuranceProviders")]
        public async Task<IActionResult> Get()
        {
            var result = await _insuranceProvidersRepository.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetInsurancesProvidersByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _insuranceProvidersRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("SaveInsuranceProviders")]
        public async Task<IActionResult> Post([FromBody] InsuranceProviders insuranceProviders)
        {
            var result = await _insuranceProvidersRepository.Save(insuranceProviders);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateInsurnaceProviders")]
        public async Task<IActionResult> Put(int id, [FromBody] InsuranceProviders insuranceProviders)
        {
            var result = await _insuranceProvidersRepository.Update(id, insuranceProviders);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("DisableInsuranceProviders")]
        public async Task<IActionResult> Delete(int id, [FromBody] InsuranceProviders insuranceProviders)
        {
            var result = await _insuranceProvidersRepository.Remove(id, insuranceProviders);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
