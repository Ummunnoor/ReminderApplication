using ReminderApplication.Entities;

namespace ReminderApplication.Interfaces.Repositories
{
    public interface IReminderRepository : IGenericRepository<Reminder>
    {
        Task<Reminder> GetReminderById(int id);
        Task<List<Reminder>> GetRemindersByUserId(int UserId);
        Task<List<Reminder>> GetRemindersByEventId(int EventId);
        Task<List<Reminder>> GetRemindersToDisplayAsync();


    }
}
