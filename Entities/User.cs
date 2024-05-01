using ReminderApplication.Contracts;

namespace ReminderApplication.Entities
{
    public class User : AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Navigation property
        public ICollection<Event> Events { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
    }
}
