using ReminderApplication.Contracts;

namespace ReminderApplication.Entities
{
    public class Admin : AuditableEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
