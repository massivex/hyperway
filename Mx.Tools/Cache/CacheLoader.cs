using System;
using System.Collections.Generic;
using System.Text;

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

//         this.cache = CacheBuilder.newBuilder()
// .maximumSize(1000)
// .expireAfterWrite(5, TimeUnit.MINUTES)
// .build(this);
        }

        public TValue this[TKey key] => this.cache.Get<TValue>(key);
    }

    public class CacheLoaderOptions
    {
        private TimeSpan DefaultExpiration { get; set; }
    }
}
