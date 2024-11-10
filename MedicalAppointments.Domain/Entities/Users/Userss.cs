using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalAppointments.Domain.Base;

namespace MedicalAppointments.Domain.Entities.Users
{
    [Table("Users", Schema = "users")]
    public sealed class Userss : BaseEntity
    {
        [Key]
        public int UserID { get; private set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
    }
}
