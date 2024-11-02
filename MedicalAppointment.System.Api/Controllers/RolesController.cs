using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Persistance.Interfaces.System;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.System.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;
        public RolesController(IRolesRepository rolesRepository) => _rolesRepository = rolesRepository;
        [HttpGet("GetRoles")]
        public async Task<IActionResult> Get()
        {
            var result = await _rolesRepository.GetAll();
            if (!result.Success) 
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("GetRolesByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _rolesRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("SaveRoles")]
        public async Task<IActionResult> Post([FromBody] Roles roles)
        {
            var result = await _rolesRepository.Save(roles);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("UpdateRoles")]
        public async Task<IActionResult> Put([FromBody] Roles roles)
        {
            var result = await _rolesRepository.Update(roles);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("DisableRoles")]
        public async Task<IActionResult> Disable(Roles roles)
        {
            var result = await _rolesRepository.Remove(roles);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
