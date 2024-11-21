namespace MedicalAppointment.Application.Dtos.Appointments.DoctorAvailability
{
    public class DoctorAvailabilityBaseDto
    {
        public int AvailabilityId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AvailableDate {  get; set; }
        public TimeOnly StarTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
