using System.Linq.Expressions;

namespace ReminderApplication.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<bool> DeleteAsync(T entity);



    }
}
