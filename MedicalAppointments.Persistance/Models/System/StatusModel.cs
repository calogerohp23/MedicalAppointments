namespace MedicalAppointments.Persistance.Models.System
{
    public sealed class StatusModel
    {
        public int StatusID { get; set; }
        public string? StatusName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
    }
}
