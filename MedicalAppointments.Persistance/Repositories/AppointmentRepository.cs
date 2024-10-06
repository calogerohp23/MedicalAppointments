using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Domain.Result;
using MedicalAppointments.Persistance.Base;
using MedicalAppointments.Persistance.Context;
using MedicalAppointments.Persistance.Interfaces.Configuration;

namespace MedicalAppointments.Persistance.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointments
    {
        public AppointmentRepository(MedicalAppointmentContext medicalAppointmentContext) : base(medicalAppointmentContext)
        {
        }

        public override Task<OperationResult> Update(Appointment entity)
        {
            return base.Update(entity);
        }
    }
}
