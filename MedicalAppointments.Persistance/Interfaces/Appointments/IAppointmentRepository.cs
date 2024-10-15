using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Repositories;

namespace MedicalAppointments.Persistance.Interfaces.Appointments
{
    public interface IAppointmentRepository:IBaseRepository<Appointment>
    {
    }
}
