using MedicalAppointments.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Medical
{
    [Table("AvailabilityModes", Schema = "medical")]
    public sealed class AvailabilityModes : BaseEntity
    {
        [Key]
        public int SAvailabilityModeID { get; private set; }
        public string? AvailabilityMode { get; set; }

    }
}
