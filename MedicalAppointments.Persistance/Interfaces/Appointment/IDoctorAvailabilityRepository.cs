using MedicalAppointments.Domain.Repositories;
using MedicalAppointments.Domain.Entities.Appointments;

namespace MedicalAppointments.Persistance.Interfaces.Appointment
{
    public interface IDoctorAvailabilityRepository: IBaseRepository<DoctorAvailability>
    {
    }
}
