using MedicalAppointments.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.System
{
    [Table("Roles", Schema = "system")]
    public sealed class Roles : BaseEntity
    {
        [Key]
        public int RoleID { get; private set; }
        public string? RoleName { get; set; }

    }
}
