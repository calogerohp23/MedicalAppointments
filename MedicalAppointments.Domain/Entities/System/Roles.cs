

namespace MedicalAppointments.Domain.Entities.System
{
    public sealed class Roles
    {
        public int RoleID { get; set; }
        public string RoleName { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

    }
}
