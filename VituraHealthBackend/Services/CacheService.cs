using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using VituraHealthBackend.Models;

namespace VituraHealthBackend.Services
{
    public class CacheService : ICacheService
    {
        private ILogger<CacheService> _logger;
        private readonly IMemoryCache _cache;

        public CacheService(ILogger<CacheService> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<T>> GetFromCache<T>(string cacheKey)
        {
            if (!_cache.TryGetValue(cacheKey, out T[]? data))
            {
                _logger.LogInformation($"Data not found in cache. Fetching from json.");
                data = await JsonReaderService.ReadAsync<T[]>(@"./Data/" + cacheKey + ".json");

                if (data != null)
                {
                    SetCache<T>(cacheKey, data);
                }
                else
                {
                    throw new Exception("Exception getting json data!");
                }
            }
            else
            {
                _logger.LogInformation($"Data retrieved from cache.");
            }

            return data.ToList();
        }

        public void SetCache<T>(string cacheKey, T[] data)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            _cache.Set(cacheKey, data, cacheEntryOptions);
            _logger.LogInformation($"Data added to cache.");  
        }
    }
}
