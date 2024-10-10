using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.System
{
    [Table("Roles", Schema = "system")]
    public sealed class Roles:Base.BaseEntity
    {
        [Key]
        public int RoleID { get; set; }
        public string? RoleName { get; set;}

    }
}
