using ReminderApplication.DTOs;

namespace ReminderApplications.Interfaces.Services
{
    public interface IMailServices
    {
        void SendEMailAsync(MailRequestDto mailRequest);
    }
}
