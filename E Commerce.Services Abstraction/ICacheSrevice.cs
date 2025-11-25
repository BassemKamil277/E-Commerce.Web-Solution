using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services_Abstraction
{
    public interface ICacheSrevice
    {
        Task<string?> GetAsync(string CacheKey);

        Task SetAsync(string CacheKey , object CacheValue, TimeSpan TimeToLive);
    }
}
