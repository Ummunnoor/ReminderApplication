using ReminderApplication.Entities;

namespace ReminderApplication.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUser(int id);
    }

}
