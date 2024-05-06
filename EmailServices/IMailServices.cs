using ReminderApplication.DTOs.RequestModels;

namespace ReminderApplication.EmailServices
{
    public interface IMailServices
    {
        public void SendEMailAsync(MailRequest mailRequest);
    }
}
