namespace MedicalAppointments.Persistance.Models.System
{
    public sealed class RolesModel
    {
        public int RoleID { get; set; }
        public string? RoleName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
