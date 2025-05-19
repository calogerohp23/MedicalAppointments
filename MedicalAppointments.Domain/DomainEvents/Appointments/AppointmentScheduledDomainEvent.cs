namespace MedicalAppointments.Domain.DomainEvents.Appointments
{
    public class AppointmentScheduledDomainEvent : INotification
    {
        public Guid ApointmentID { get; }
    }
}
