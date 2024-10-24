using MedicalAppointments.Domain.Entities.Appointments;
using MedicalAppointments.Persistance.Validator.Base;

namespace MedicalAppointments.Persistance.Validator.Entities.Appointments
{
    public class AppointmentValidator : BaseValidator<Appointment>
    {

        public override Task ValidateAsync(Appointment entity)
        {
            throw new NotImplementedException();
        }
    }
}
