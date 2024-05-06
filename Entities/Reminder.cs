using ReminderApplication.Contracts;
using ReminderApplication.Entities.Enums;

namespace ReminderApplication.Entities
{
    public class Reminder : AuditableEntity
    {
        public int ReminderID { get; set; }
        public int EventID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PhoneNumer { get; set; }
        public DateTime ReminderDateTime { get; set; } = DateTime.Now;
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string? Notification { get; set; }
        public EventStatus Status { get; set; }

        // Navigation property
        public Event Event { get; set; }
        public User User { get; set; }
    }
}
