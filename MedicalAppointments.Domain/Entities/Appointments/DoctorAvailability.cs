using MedicalAppointments.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Appointments
{
    [Table("DoctorAvailability", Schema = "appointments")]
    public sealed class DoctorAvailability : BaseEntity
    {
        [Key]
        public int AvailabilityId { get; set; }
        public int DoctorID { get; set; }
        public DateTime AvailableDate { get; set; }
        public TimeOnly StartTime {  get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
