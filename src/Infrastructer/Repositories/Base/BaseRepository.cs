using Core.Interfaces.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        protected readonly EventApiDbContext _dbContext;
        private readonly DbSet<T> dbSet;

        public BaseRepository(EventApiDbContext context)
        {
            _dbContext = context;
            dbSet = _dbContext.Set<T>();
        }

        public async Task<int> CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();

            // Entity'yi detached duruma getir
            _dbContext.Entry(entity).State = EntityState.Detached;

            return result;
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await dbSet.FindAsync(id);
            if(entity != null) 
            {
                _dbContext.Remove(entity); 
                return await _dbContext.SaveChangesAsync();
            }
            return -1;
        }
        
        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Update(entity); 
            return await _dbContext.SaveChangesAsync();
        }


        public async Task<IEnumerable<T>?> GetAllAsync(Expression<Func<T, bool>> predicate)
        =>await dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id)
       => await dbSet.AsNoTracking().FirstOrDefaultAsync(entity=>entity.Id == id);



    }
}
