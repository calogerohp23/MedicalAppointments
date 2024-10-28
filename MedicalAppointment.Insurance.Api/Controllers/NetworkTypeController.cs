using MedicalAppointments.Persistance.Interfaces.Appointment;
using MedicalAppointments.Persistance.Interfaces.Insurance;
using MedicalAppointments.Persistance.Repositories.Appointments;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeController : ControllerBase
    {
        private readonly INetworkTypeRepository _networkTypeRepository;
        public NetworkTypeController(INetworkTypeRepository networkTypeRepository) => _networkTypeRepository = networkTypeRepository;

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

        [HttpGet("G")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
