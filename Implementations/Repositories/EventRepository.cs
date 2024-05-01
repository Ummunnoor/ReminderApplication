using Microsoft.EntityFrameworkCore;
using ReminderApplication.Context;
using ReminderApplication.Entities;
using ReminderApplication.Interfaces.Repositories;

namespace ReminderApplication.Implementations.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationContext Context)
        {
            _Context = Context;
        }
        public async Task<List<Event>> GetAllEventsByUserIdAsync(int Id)
        {
            return await _Context.Events
            .Where(x => x.UserId == Id)
            .Include(x => x.User)
            .ToListAsync();

        }
        public async Task<List<Event>> GetEventsByDate(DateTime date)
        {
            return await _Context.Events
            .Where(e => e.StartDateTime == date)
            .Include(e => e.User)
            .ToListAsync();
        }
        public async Task<List<Event>> GetEventsToDisplayAsync(int UserId)
        {
            return await _Context.Events
            .Where(x => x.IsDeleted == false)
            .Include(x => x.User)
            .ToListAsync();
        }
    }
}
