using System;

namespace Mx.Tools.Cache
{
    using Microsoft.Extensions.Caching.Memory;

    public class CacheLoader<TKey, TValue>
    {
        private readonly IMemoryCache cache;

        public CacheLoader()
        {
            this.cache = new MemoryCache(new MemoryCacheOptions()
                                             {
                                                 ExpirationScanFrequency = TimeSpan.FromSeconds(5),
                                                 SizeLimit = 1000
                                             });
        }

        public TValue this[TKey key] => this.cache.Get<TValue>(key);
    }
}
