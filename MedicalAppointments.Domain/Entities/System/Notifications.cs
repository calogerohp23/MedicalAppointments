

namespace MedicalAppointments.Domain.Entities.System
{
    public sealed class Notifications
    {
        public int NotificationId {  get; set; }
        public int UserId {  get; set; }
        public string Message {  get; set; }
        public DateTime? SentAt { get; set; }
    }
}
