namespace MedicalAppointments.Persistance.Models.Appointments
{
    public class AppointmentPatientDoctorStatusModel
    {
        public int AppointmentID { get; set; }
        public string? Patient { get; set; }
        public string? Doctor { get; set; }
        public string? Status { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
