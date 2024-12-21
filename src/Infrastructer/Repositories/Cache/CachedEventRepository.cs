using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Repositories.Cache
{
    public class CachedEventRepository(
        EventRepository _decorated,
        IMemoryCache _cache) : IEventRepository
    {
        private static readonly List<string> CachedKeys = [];

        public async Task<int> CreateAsync(Event entity)
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
        
        public async Task<int> UpdateAsync(Event entity)
        {
            int result = await _decorated.UpdateAsync(entity); 
            RemoveAllCachedItems(result);
            return result;
        }

        public async Task<IEnumerable<Event>?> GetAllAsync(Expression<Func<Event, bool>> predicate)
        {
            string key = $"all-events-{predicate}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetAllAsync(predicate);
            });

        }

        public async Task<Event?> GetByIdAsync(Guid id)
        {
            string key = $"event-{id}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetByIdAsync(id);
            });
        }
        public async Task<PaginationDto<Event>?> GetAllEventsWithPaginationAsync(int page, int pageSize)
        {
            string key = $"events-page-{page}-size-{pageSize}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetAllEventsWithPaginationAsync(page, pageSize);
            });
        }

        public async Task<Event?> GetEventWithParticipantsAsync(Guid eventId)
        {
            string key = $"with-participants-{eventId}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetEventWithParticipantsAsync(eventId);
            });
        }

        public async Task<int> GetParticipantCountAsync(Guid eventId)
        {
            string key = $"with-participantCount-{eventId}";
            return await _cache.GetOrCreateAsync(key, async entry =>
            {
                CachedKeys.Add(key);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(3));
                return await _decorated.GetParticipantCountAsync(eventId);
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
