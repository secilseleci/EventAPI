using Core.Interfaces.Entities;
using System.Linq.Expressions;

namespace Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class, IBaseEntity
    {
        Task<int> CreateAsync(T entity);
        Task<int> DeleteAsync(Guid Id);
        Task<int> UpdateAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> predicate);
    }

}
