using MedicalAppointments.Domain.Entities.Users;
using MedicalAppointments.Domain.Repositories;

namespace MedicalAppointments.Persistance.Interfaces.Users
{
    public interface IPatientsRepository: IBaseRepository<Patients>
    {
    }
}
