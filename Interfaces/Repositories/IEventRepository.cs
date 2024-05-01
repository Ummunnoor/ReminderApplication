using ReminderApplication.Entities;
using ReminderApplication.Implementations.Repositories;

namespace ReminderApplication.Interfaces.Repositories
{
    
        public interface IEventRepository : IGenericRepository<Event>
        {
            Task<List<Event>> GetAllEventsByUserIdAsync(int Id);

            Task<List<Event>> GetEventsByDate(DateTime date);
            Task<List<Event>> GetEventsToDisplayAsync(int UserId);
        }
    }

