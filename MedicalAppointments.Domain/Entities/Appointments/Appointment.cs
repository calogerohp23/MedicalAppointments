
namespace MedicalAppointments.Domain.Entities.Appointments
{
    public sealed class Appointment: Base.Appointments.BaseEntity
    {
        public int AppointmentID {  get; set; }
        public int PatientID { get; set; }
        public DateTime AppointmentDate {  get; set; }
        public int StatusID {get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime? UpdatedAt {  get; set; }
    }
}
