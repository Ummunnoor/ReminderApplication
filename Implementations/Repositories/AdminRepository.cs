using Microsoft.EntityFrameworkCore;
using ReminderApplication.Context;
using ReminderApplication.Entities;
using ReminderApplication.Interfaces.Repositories;

namespace ReminderApplication.Implementations.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext Context)
        {
            _Context = Context;
        }

        public async Task<List<Admin>> GetAllAdminsAsync()
        {
            return await _Context.Admins
            .Include(admin => admin.User)
            .Where(x => x.IsDeleted == false)
            .ToListAsync();
        }
        public async Task<Admin> GetByIdAsync(int id)
        {
            return await _Context.Admins
            .Include(admin => admin.User)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
        }
    }
}

    
