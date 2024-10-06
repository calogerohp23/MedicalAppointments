using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Insurance
{
    [Table("NetworkType", Schema = "Insurance")]
    public sealed class NetworkType: Base.Insurance.BaseEntity
    {
        [Key]
        public new int NetworkTypeID { get; set; }
        public string? Description {  get; set; }
    }
}
