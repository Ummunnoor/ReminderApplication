using ReminderApplication.Entities.Enums;

namespace ReminderApplication.DTOs.RequestModels
{
    public class UpdateReminderRequestModel
    {
        public int ReminderId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public EventStatus Status { get; set; }
        public DateTime EventDate { get; set; }
    }
}
