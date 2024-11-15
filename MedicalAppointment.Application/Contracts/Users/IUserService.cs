using MedicalAppointment.Application.Base;
using MedicalAppointment.Application.Dtos.Users.Users;
using MedicalAppointment.Application.Response.Users.Users;

namespace MedicalAppointment.Application.Contracts.Users
{
    public interface IUserService : IBaseService<UsersResponse,UsersSaveDto,UsersUpdateDto>
    {
    }
}
