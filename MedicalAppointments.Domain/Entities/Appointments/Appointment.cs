
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Appointments
{
    [Table("Appointments",Schema="appointments")]
    public sealed class Appointment: Base.Appointments.BaseEntity
    {
        [Key]
        public int AppointmentID {  get; set; }
        public int PatientID { get; set; }
        public DateTime AppointmentDate {  get; set; }
        public int StatusID {get; set; }
        public DateTime CreatedAt {  get; set; }
        public DateTime? UpdatedAt {  get; set; }
    }

    [Table("Appointments", Schema = "appointments")]
    public sealed class CopyOfAppointment : Base.Appointments.BaseEntity
    {
        [Key]
        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
