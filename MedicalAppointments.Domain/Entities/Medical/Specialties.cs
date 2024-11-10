using MedicalAppointments.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Medical
{
    [Table("Specialties", Schema = "medical")]
    public sealed class Specialties : BaseEntity
    {
        [Key]
        public int SpecialtyID {  get; private set; }
        public string? SpecialtyName { get; set; }

    }
}
