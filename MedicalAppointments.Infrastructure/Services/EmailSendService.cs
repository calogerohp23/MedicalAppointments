using MedicalAppointments.Infrastructure.Interfaces;
using MedicalAppointments.Infrastructure.Models;
using MedicalAppointments.Infrastructure.Results;
using System.Net;
using System.Net.Mail;

namespace MedicalAppointments.Infrastructure.Services
{
    public class EmailSendService : INotificationService
    {
        public async Task<NotificationResult> SendEmailAsync(EmailModel emailModel)
        {
            NotificationResult result = new();
            try
            {
                using var client = new SmtpClient();
                client.Host = "";
                client.Port = 0;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("user", "pwd");

                var message = new MailMessage(emailModel.From!, emailModel.To!);
                message.Body = emailModel.Body;
                message.IsBodyHtml = true;
                message.Subject = emailModel.Subject;

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                result.Message = $"There was an error sending the notification {ex.Message}";
            }
            return result;
        }

        public Task<NotificationResult> SendPushNotification(PushModel push)
        {
            throw new NotImplementedException();
        }

        public Task<NotificationResult> SendSmsAsync(SmsModel sms)
        {
            throw new NotImplementedException();
        }
    }
}
