
using MedicalAppointments.Domain.Base.Appointments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Medical
{
    [Table("AvailabilityModes", Schema = "medical")]
    public sealed class AvailabilityModes : Base.Medical.BaseEntity
    {
        [Key]
        public int SAvailabilityModeId { get; set; }
        public string AvailabilityMode { get; set; }

    }
}
