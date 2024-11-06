using MedicalAppointments.Domain.Repositories;
using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Interfaces.Appointment
{
    public interface IAppointmentRepository : IBaseRepository<Domain.Entities.Appointments.Appointment>
    {
        Task<OperationResult> Update(int id, Domain.Entities.Appointments.Appointment entity);
    }
}
