using MedicalAppointments.Domain.Repositories;


namespace MedicalAppointments.Persistance.Interfaces.Appointment
{
    public interface IAppointmentRepository : IBaseRepository<Domain.Entities.Appointments.Appointment>
    {
    }
}
