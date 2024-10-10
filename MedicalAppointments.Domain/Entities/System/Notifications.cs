using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalAppointments.Domain.Entities.System
{
    [Table("Notifications", Schema = "system")]
    public sealed class Notifications
    {
        [Key]
        public int NotificationId {  get; set; }
        public int UserId {  get; set; }
        public string? Message {  get; set; }
        public DateTime? SentAt { get; set; }
    }
}
