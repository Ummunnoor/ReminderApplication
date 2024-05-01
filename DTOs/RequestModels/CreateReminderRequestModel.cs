using ReminderApplication.Entities.Enums;

namespace ReminderApplication.DTOs.RequestModels
{
    public class CreateReminderRequestModel
    {
        public int EventID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime ReminderDateTime { get; set; }
        public string Notification { get; set; }
        public EventStatus Status { get; set; }
    }
}
