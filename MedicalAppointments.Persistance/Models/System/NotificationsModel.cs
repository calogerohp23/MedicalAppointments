namespace MedicalAppointments.Persistance.Models.System
{
    public sealed class NotificationsModel
    {
        public int NotificationsID { get; set; }
        public string? UserName { get; set; }
        public string? Message { get; set; }
        public DateTime? SentAt { get; set; }

    }
}
