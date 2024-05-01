using ReminderApplication.Entities;
using ReminderApplication.Implementations.Repositories;

namespace ReminderApplication.Interfaces.Repositories
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<List<Admin>> GetAllAdminsAsync();
        Task<Admin> GetByIdAsync(int id);

    }
}
