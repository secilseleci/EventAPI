using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories.Implementations;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Cache
{
    public class CachedInvitationRepository(
        InvitationRepository _decorated,
        IMemoryCache _cache) : IInvitationRepository
    {
        private static readonly List<string> CachedKeys = [];

        public async Task<int> CreateAsync(Invitation entity)
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
        
        public async Task<int> UpdateAsync(Invitation entity)
        {
            int result = await _decorated.UpdateAsync(entity); 
            RemoveAllCachedItems(result);
            return result;
        }

        public async Task<IEnumerable<Invitation>?> GetAllAsync(Expression<Func<Invitation, bool>> predicate)
        {
            string key = $"all-invitations-{predicate}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetAllAsync(predicate);
            });

        }

        public async Task<Invitation?> GetByIdAsync(Guid id)
        {
            string key = $"invitation-{id}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetByIdAsync(id);
            });
        }

        public async Task<Invitation?> GetInvitationByEventAndReceiverAsync(Guid eventId, Guid receiverId)
        {
            string key = $"invitation-{eventId}-{receiverId}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetInvitationByEventAndReceiverAsync(eventId,receiverId);
            });
        }
        public async Task<List<Invitation>?> GetInvitationsWithDetailsAsync(Expression<Func<Invitation, bool>> predicate)
        {
            string key = $"invitation-with-details-{predicate}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetInvitationsWithDetailsAsync(predicate);
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
