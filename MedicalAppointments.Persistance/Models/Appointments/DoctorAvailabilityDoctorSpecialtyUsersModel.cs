namespace MedicalAppointments.Persistance.Models.Appointments
{
    public sealed class DoctorAvailabilityDoctorSpecialtyUsersModel
    {
        public int AvailabilityID { get; set; }
        public int DoctorId { get; set; }
        public string? FullName { get; set; }
        public string? Specialty { get; set; }
        public DateTime? AvailableDate { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
    }
}
