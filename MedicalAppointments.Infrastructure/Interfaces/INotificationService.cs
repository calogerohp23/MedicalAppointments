using MedicalAppointments.Infrastructure.Models;
using MedicalAppointments.Infrastructure.Results;

namespace MedicalAppointments.Infrastructure.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResult> SendEmailAsync(EmailModel email);
        Task<NotificationResult> SendSmsAsync(SmsModel sms);
        Task<NotificationResult> SendPushNotification(PushModel push);
    }
}
