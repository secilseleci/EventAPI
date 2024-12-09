using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Cache
{
    public class CachedUserRepository(
        UserRepository _decorated,
        IMemoryCache _cache) : IUserRepository
    {
        private static readonly List<string> CachedKeys = [];

        public async Task<int> CreateAsync(User entity)
        {
            int result = await _decorated.CreateAsync(entity);
            RemoveAllCachedItems(result);
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            int result = await _decorated.DeleteAsync(id);
            RemoveAllCachedItems(result);
            return result;
        }

        public async Task<int> UpdateAsync(User entity)
        {
            int result = await _decorated.UpdateAsync(entity);
            RemoveAllCachedItems(result);
            return result;
        }

        public async Task<IEnumerable<User>?> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            string key = $"all-events-{predicate}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetAllAsync(predicate);
            });

        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            string key = $"user-{id}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetByIdAsync(id);
            });
        }



        #region Helper Methods
        private void RemoveAllCachedItems(int result)
        {
            if (result > 0)
            {
                foreach (var key in CachedKeys)
                {
                    _cache.Remove(key);
                }
            }

            CachedKeys.Clear();
        }
        #endregion  
    }
}
