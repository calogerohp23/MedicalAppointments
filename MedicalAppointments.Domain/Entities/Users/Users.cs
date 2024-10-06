using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.Users
{
    [Table("Users", Schema = "users")]
    public sealed class Users: Base.Users.BaseEntity
    {
        [Key]
        public int UserId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId {  get; set; }
    }
}
