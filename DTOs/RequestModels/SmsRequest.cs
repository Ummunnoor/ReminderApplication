namespace ReminderApplication.DTOs.RequestModels
{
    public class SmsRequest
    {
        public string Recipient { get; set; }
        public string Content { get; set; }
    }
}
