using ReminderApplication.Contracts;

namespace ReminderApplication.Entities
{
    public class Event : AuditableEntity
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        // Navigation property
        public User User { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
    }
}
