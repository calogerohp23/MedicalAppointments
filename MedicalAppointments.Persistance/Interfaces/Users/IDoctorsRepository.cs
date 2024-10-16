using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Repositories;

namespace MedicalAppointments.Persistance.Interfaces.Users
{
    public interface IDoctorsRepository: IBaseRepository<Doctors>
    {
    }
}
