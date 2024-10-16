using MedicalAppointments.Domain.Repositories;

namespace MedicalAppointments.Persistance.Interfaces.Users
{
    public interface IUsersRepository: IBaseRepository<Domain.Entities.Users.Users>
    {
    }
}
