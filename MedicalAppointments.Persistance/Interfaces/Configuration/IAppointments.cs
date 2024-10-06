using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Repositories;

namespace MedicalAppointments.Persistance.Interfaces.Configuration
{
    public interface IAppointments: IBaseRepository<Appointment>
    {

    }
}
