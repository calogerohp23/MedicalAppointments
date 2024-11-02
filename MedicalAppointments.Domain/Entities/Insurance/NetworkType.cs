using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Insurance
{
    [Table("NetworkType", Schema = "Insurance")]
    public sealed class NetworkType : Base.BaseEntity
    {
        [Key]
        public int NetworkTypeID { get; private set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
