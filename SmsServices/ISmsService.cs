using ReminderApplication.DTOs.RequestModels;

namespace ReminderApplication.SmsServices
{
    public interface ISmsService
    {
        public void SendSmsAsync(SmsRequest smsRequest);
    }
}
