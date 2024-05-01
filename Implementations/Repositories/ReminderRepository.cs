using Microsoft.EntityFrameworkCore;
using ReminderApplication.Context;
using ReminderApplication.Entities;
using ReminderApplication.Entities.Enums;
using ReminderApplication.Interfaces.Repositories;

namespace ReminderApplication.Implementations.Repositories
{
    public class ReminderRepository : GenericRepository<Reminder>, IReminderRepository
    {
        public ReminderRepository(ApplicationContext Context)
        {
            _Context = Context;
        }
        public async Task<Reminder> GetReminderById(int id)
        {
            return await _Context.Reminders
            .Where(r => r.Id == id)
            .Include(r => r.Event)
            .FirstAsync();
        }
        public async Task<List<Reminder>> GetRemindersByUserId(int UserId)
        {
            return await _Context.Reminders
            .Where(r => r.Id == UserId)
            .Include(r => r.Event)
            .ToListAsync();
        }
        public async Task<List<Reminder>> GetRemindersByEventId(int EventId)
        {
            return await _Context.Reminders
            .Where(r => r.Id == EventId)
            .Include(r => r.User)
            .ToListAsync();
        }
        public async Task<List<Reminder>> GetRemindersToDisplayAsync()
        {
            return await _Context.Reminders
            .Where(r => r.Status == EventStatus.Pending && r.EventDate <= DateTime.Now && r.IsDeleted == false)
            .Include(r => r.Event)
            .Include(r => r.User)
            .ToListAsync();
        }

    }
}