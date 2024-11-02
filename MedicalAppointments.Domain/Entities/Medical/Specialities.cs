using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Medical
{
    [Table("Specialities", Schema = "medical")]
    public sealed class Specialities : Base.BaseEntity
    {
        [Key]
        public int SpecialtyID {  get; private set; }
        public string? SpecialtyName { get; set; }

    }
}
