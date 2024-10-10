using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Repositories;
using MedicalAppointments.Domain.Result;

namespace MedicalAppointments.Persistance.Interfaces.Configuration
{
    public interface IAppointmentsRepository: IBaseRepository<Appointment>
    {
        List<OperationResult> GetAppointmentByUserID(int userID);
    }
}
