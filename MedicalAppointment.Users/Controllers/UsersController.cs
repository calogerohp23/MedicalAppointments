using MedicalAppointments.Domain.Entities.System;
using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Persistance.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalAppointment.Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {       
       
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository) => _usersRepository = usersRepository;
        [HttpGet("GetUsers")]
        public async Task<IActionResult> Get()
        {
            var result = await _usersRepository.GetAll();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("GetUsersByID")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _usersRepository.GetEntityBy(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("SaveUsers")]
        public async Task<IActionResult> Post([FromBody] MedicalAppointments.Domain.Entities.Users.Users users)
        {
            var result = await _usersRepository.Save(users);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> Put([FromBody] MedicalAppointments.Domain.Entities.Users.Users users)
        {
            var result = await _usersRepository.Update(users);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("DisableUsers")]
        public async Task<IActionResult> Disable(MedicalAppointments.Domain.Entities.Users.Users users)
        {
            var result = await _usersRepository.Remove(users);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
