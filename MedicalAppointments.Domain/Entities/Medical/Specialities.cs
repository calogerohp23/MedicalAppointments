using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Medical
{
    [Table("Specialities", Schema = "medical")]
    public sealed class Specialities : Base.Medical.BaseEntity
    {
        [Key]
        public int SpecialtyID {  get; set; }
        public string SpecialtyName { get; set; }

    }
}
