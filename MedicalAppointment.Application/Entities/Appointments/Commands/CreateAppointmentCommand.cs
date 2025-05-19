using MediatR;

namespace MedicalAppointments.Application.Entities.Appointments.Commands
{
    public class CreateAppointmentCommand: IRequest<Guid>
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}
