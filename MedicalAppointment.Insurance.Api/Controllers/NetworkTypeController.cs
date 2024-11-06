using MedicalAppointments.Domain.Entities.Insurance;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeController(INetworkTypeRepository networkTypeRepository) : ControllerBase
    {
        private readonly INetworkTypeRepository _networkTypeRepository = networkTypeRepository;

        [HttpGet("GetNetworkType")]
        public async Task<IActionResult> Get()
        {
            var result = await _networkTypeRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetNetworkTypeByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _networkTypeRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveNetworkType")]
        public async Task<IActionResult> Post([FromBody] NetworkType networkType)
        {
            var result = await _networkTypeRepository.Save(networkType);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateNetworkType")]
        public async Task<IActionResult> Put(int id, [FromBody] NetworkType networkType)
        {
            var result = await _networkTypeRepository.Update(id, networkType);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableNetworkType")]
        public async Task<IActionResult> Delete(int id, [FromBody] NetworkType networkType)
        {
            var result = await _networkTypeRepository.Remove(id, networkType);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
