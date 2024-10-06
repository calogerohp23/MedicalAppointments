using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.System
{
    [Table("Roles", Schema = "system")]
    public sealed class Roles
    {
        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

    }
}
