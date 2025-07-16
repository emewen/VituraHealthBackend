using VituraHealthBackend.Models;

namespace VituraHealthBackend.Services
{
    public interface ICacheService
    {
        public Task<List<T>> GetFromCache<T>(string cacheKey);
        public void SetCache<T>(string cacheKey, T[] data);
    }
}
