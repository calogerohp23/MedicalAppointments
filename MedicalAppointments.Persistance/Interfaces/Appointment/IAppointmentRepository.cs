using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Repositories;

namespace MedicalAppointments.Persistance.Interfaces.Appointment
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
    }
}
