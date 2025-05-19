namespace MedicalAppointments.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public DateTime ScheduledDate { get; private set; }
        public AppointmentStatus Status { get; private set; }

        public Appointment(Guid patientId, Guid doctorID, DateTime scheduledDate)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            DoctorId = doctorID;
            ScheduledDate = scheduledDate;
            Status = AppointmentStatus.Pending;
        }
        public void Confirm() => Status = AppointmentStatus.Confirmed;
        public void Cancel() => Status = AppointmentStatus.Cancelled;
        public void Reschedule(DateTime newDate)
        {
            if (newDate < DateTime.Now)
                throw new InvalidOperationException("Cannot reschedule to a past date.");
            ScheduledDate = newDate;
        }
    }
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Rescheduled,
        Completed
    }
}
