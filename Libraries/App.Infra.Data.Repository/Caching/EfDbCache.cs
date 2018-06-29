using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using App.Core.Caching;
using App.Core.Infrastructure.DependencyManagement;
using static System.String;

namespace App.Infra.Data.Repository.Caching
{
    public class EfDbCache : IDbCache
    {
        private const string Keyprefix = "efcache:";
        private readonly object _lock = new object();

        private bool _enabled;

        private readonly ICacheManager _cache;
        private readonly Work<IRequestCache> _requestCache;

        public EfDbCache(ICacheManager innerCache, Work<IRequestCache> requestCache)
        {
            _cache = innerCache;
            _requestCache = requestCache;

            _enabled = true;
        }

        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value)
                    return;

                lock (_lock)
                {
                    if (_enabled == false && value)
                    {
                        // When cache was disabled previously and gets enabled,
                        // we should clear the cache, because no invalidation has been performed
                        // during disabled state. We would deal with stale data otherwise.
                        Clear();
                    }
                    _enabled = value;
                }
            }
        }

        public void Clear()
        {
            _cache.RemoveByPattern(Keyprefix);
            //_requestCache.Value.RemoveByPattern(KEYPREFIX);
        }

        public virtual bool TryGet(string key, out object value)
        {
            value = null;

            if (!Enabled)
            {
                return false;
            }

            key = HashKey(key);
            var now = DateTime.UtcNow;

            var entry = _cache.Get<DbCacheEntry>(key);

            if (entry != null)
            {
                if (entry.HasExpired(now))
                {
                    lock (Intern(key))
                    {
                        InvalidateItemUnlocked(entry);
                    }
                }
                else
                {
                    value = entry.Value;
                    return true;
                }
            }

            return false;
        }

        private static string HashKey(string key)
        {
            // Looking up large Keys can be expensive (comparing Large Strings), so if keys are large, hash them, otherwise if keys are short just use as-is
            if (key.Length <= 128)
                return Keyprefix + "data:" + key;

            using (var sha = new SHA1CryptoServiceProvider())
            {
                key = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(key)));
                return Keyprefix + "data:" + key;
            }
        }

        protected void InvalidateItemUnlocked(DbCacheEntry entry)
        {
            // remove item itself from cache
            _cache.Remove(entry.Key);

            // remove this key in all lookups
            foreach (var set in entry.EntitySets)
            {
                var lookup = GetLookupSet(set, false);
                lookup?.Remove(entry.Key);
            }
        }

        private ISet GetLookupSet(string entitySet, bool create = true)
        {
            var key = GetLookupKeyFor(entitySet);

            if (create)
            {
                return _cache.GetHashSet(key);
            }

            if (_cache.Contains(key))
            {
                return _cache.GetHashSet(key);
            }

            return null;
        }

        private string GetLookupKeyFor(string entitySet)
        {
            return Keyprefix + "lookup:" + entitySet;
        }

        public virtual DbCacheEntry Put(string key, object value, IEnumerable<string> dependentEntitySets, TimeSpan? duration)
        {
            if (!Enabled )
            {
                return null;
            }

            key = HashKey(key);

            lock (Intern(key))
            {
                var entitySets = dependentEntitySets.Distinct().ToArray();
                var entry = new DbCacheEntry
                {
                    Key = key,
                    Value = value,
                    EntitySets = entitySets,
                    CachedOnUtc = DateTime.UtcNow,
                    Duration = duration
                };

                _cache.Put(key, entry);

                foreach (var entitySet in entitySets)
                {
                    var lookup = GetLookupSet(entitySet);
                    lookup.Add(key);
                }

                return entry;
            }
        }

        public virtual bool RequestTryGet(string key, out DbCacheEntry value)
        {
            value = null;

            if (!Enabled)
            {
                return false;
            }

            key = HashKey(key);

            value = _requestCache.Value.Get<DbCacheEntry>(key);
            return value != null;
        }

        public virtual DbCacheEntry RequestPut(string key, object value)
        {
            if (!Enabled )
            {
                return null;
            }

            key = HashKey(key);

            var entry = new DbCacheEntry
            {
                Key = key,
                Value = value,
                EntitySets = null,
                CachedOnUtc = DateTime.UtcNow
            };

            _requestCache.Value.Put(key, entry);

            //foreach (var entitySet in entry.EntitySets)
            //{
            //    var lookup = RequestGetLookupSet(entitySet);
            //    lookup.Add(key);
            //}

            return entry;
        }

        private HashSet<string> RequestGetLookupSet(string entitySet, bool create = true)
        {
            var key = GetLookupKeyFor(entitySet);

            if (create)
            {
                return _requestCache.Value.Get(key, () => new HashSet<string>());
            }

            return _requestCache.Value.Get<HashSet<string>>(key);
        }
    }
}
