using Microsoft.EntityFrameworkCore;
using ReminderApplication.Context;
using ReminderApplication.Interfaces.Repositories;
using System.Linq.Expressions;

namespace ReminderApplication.Implementations.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        protected ApplicationContext _Context;
        public async Task<T> CreateAsync(T entity)
        {
            await _Context.Set<T>().AddAsync(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _Context.Set<T>().Update(entity);
            await _Context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> GetAsync(int id)
        {
            return await _Context.Set<T>().FindAsync(id);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _Context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            return await _Context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _Context.Set<T>().Remove(entity);
            await _Context.SaveChangesAsync();
            return true;
        }
    }
}
