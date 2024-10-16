
namespace MedicalAppointments.Persistance.Models
{
    public class AppointmentPatientDoctorStatusModel
    {
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int StatusID {  get; set; }
        public string? Patient {  get; set; }
        public string? Doctor {  get; set; }
        public string? Status {  get; set; }
        public DateTime? AppointmentDate {get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
