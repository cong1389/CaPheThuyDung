﻿using System;
using System.Collections.Generic;
using System.Linq;
using App.Core.Extensions;

namespace App.Core.Caching
{
    [Serializable]
    internal class MemorySet : HashSet<string>, ISet
    {
        private readonly ICacheManager _cache;

        public MemorySet(ICacheManager cache)
        {
            _cache = cache;
        }

        public bool Move(string destinationKey, string item)
        {
            if (Contains(item))
            {
                var target = _cache.GetHashSet(destinationKey);
                return target.Add(item);
            }

            return false;
        }

        public long ExceptWith(params string[] keys)
        {
            return Combine(this.Except, keys);
        }

        public long IntersectWith(params string[] keys)
        {
            return Combine(this.Intersect, keys);
        }

        public long UnionWith(params string[] keys)
        {
            return Combine(this.Union, keys);
        }

        private long Combine(Func<IEnumerable<string>, IEnumerable<string>> func, params string[] keys)
        {
            if (keys.Length == 0)
                return 0;

            var other = keys.SelectMany(x => _cache.GetHashSet(x)).Distinct();
            func(other);

            Clear();
            this.AddRange(other);

            return Count;
        }
    }
}
