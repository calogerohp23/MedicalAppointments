using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Configuration;

namespace MedicalAppointments.Persistance.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentsRepository
    {
        public AppointmentRepository(MedicalAppointmentContext medicalAppointmentContext) : base(medicalAppointmentContext)
        {
        }

        public List<OperationResult> GetAppointmentByUserID(int userID)
        {
            throw new NotImplementedException();
        }

    }
}
