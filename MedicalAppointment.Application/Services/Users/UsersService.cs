using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dtos.Users.Users;
using MedicalAppointment.Application.Response.Users.Users;
using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Persistance.Interfaces.Users;
using Microsoft.Extensions.Logging;

namespace MedicalAppointment.Application.Services.Users
{
    public class UsersService : IBaseService<UsersResponse, UsersSaveDto, UsersUpdateDto>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersService> _logger;
        public UsersService(IUsersRepository usersRepository,
            ILogger<UsersService> logger)
        {
            if (usersRepository is null)
            {
                throw new ArgumentNullException(nameof(usersRepository));
            }
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public async Task<UsersResponse> GetAll()
        {
            UsersResponse response = new();
            try
            {
                var result = await _usersRepository.GetAll();
                List<UsersGetAllDto> users = ((List<Userss>)result.Data)
                                            .Select(users => new UsersGetAllDto()
                                            {
                                                FirstName = users.FirstName,
                                                LastName = users.LastName,
                                                Email = users.Email,
                                                RoleId = users.RoleId,
                                                DateChange = users.UpdatedAt,
                                                UserChange = users

                                            }).ToList();



                response.IsSuccess = result.Success;
                response.Model = result.Data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The users couldn't be obtained.";
                _logger.LogError(response.Message,
                                 ex.ToString());
            }
            return response;
        }

        public async Task<UsersResponse> GetById(int id)
        {
            UsersResponse response = new();
            try
            {
                var result = await _usersRepository.GetEntityBy(id);
                response.IsSuccess = result.Success;
                response.Model = result.Data;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The user couldn't be found.";
                _logger.LogError(response.Message,
                                 ex.ToString());
            }
            return response;
        }

        public async Task<UsersResponse> SaveAsync(UsersSaveDto dto)
        {
            UsersResponse response = new();
            try
            {
                Userss users = new()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Password = dto.Password,
                    RoleId = dto.RoleId,
                    CreatedAt = dto.CreatedAt,
                    CreatedBy = dto.CreatedBy
                };
                var result = await _usersRepository.Save(users);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "The user couldn't be saved.";
                _logger.LogError(response.Message,
                                 ex.ToString());
            }
            return response;
        }

        public Task<UsersResponse> UpdateAsync(UsersUpdateDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
