using E_Commerce.Domain.Contracts;
using E_Commerce.Services_Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class CacheSrevice : ICacheSrevice
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheSrevice(ICacheRepository cacheRepository)
        {
           _cacheRepository = cacheRepository;
        }

        public async Task<string?> GetAsync(string CacheKey)
        {
          return await _cacheRepository.GetAsync(CacheKey);
        }

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
          var Value  = JsonSerializer.Serialize(CacheValue , new JsonSerializerOptions()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase
          });
            
            await _cacheRepository.SetAsync(CacheKey, Value, TimeToLive);
        }
    }
}