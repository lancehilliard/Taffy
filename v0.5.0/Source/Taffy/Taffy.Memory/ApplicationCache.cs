using System;
using System.Web;
using System.Web.Caching;

namespace Taffy.Memory {
    public class ApplicationCache : IApplicationCache {
        private readonly Cache _cache = HttpRuntime.Cache;

        public void Add(string key, object value, DateTime absoluteExpiration) {
            _cache.Add(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public void Add(string key, object value, TimeSpan slidingExpiration) {
            _cache.Add(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, CacheItemPriority.Default, null);
        }

        public object Get(string key) {
            var result = _cache.Get(key);
            return result;
        }

        public void Remove(string key) {
            _cache.Remove(key);
        }

        public void Add(string key, object value) {
            _cache.Insert(key, value);
        }
    }

    public interface IApplicationCache {
        void Add(string key, object value, DateTime absoluteExpiration);
        void Add(string key, object value, TimeSpan slidingExpiration);
        object Get(string key);
        void Remove(string key);
        void Add(string key, object value);
    }
}