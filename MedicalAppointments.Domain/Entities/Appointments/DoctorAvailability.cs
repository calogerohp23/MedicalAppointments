
namespace MedicalAppointments.Domain.Entities.Appointments
{
    public sealed class DoctorAvailability: Base.Appointments.BaseEntity
    {
        public int AvailabilityId { get; set; }
        public int DoctorID { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeOnly StartTime {  get; set; }
        public TimeOnly EndTime { get; set; }

    }
}
