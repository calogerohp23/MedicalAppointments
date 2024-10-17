using MedicalAppointments.Domain.Repositories;
using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Interfaces.Appointment
{
    public interface IAppointmentRepository : IBaseRepository<Domain.Entities.Appointments.Appointment>
    {
    }
}
