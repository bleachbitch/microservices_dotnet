﻿using System.Collections.Concurrent;

namespace ShopingCart
{
    public class Cache : ICache
    {
        private static IDictionary<string, Tuple<DateTimeOffset, object>> cache = new ConcurrentDictionary<string, Tuple<DateTimeOffset, object>>();

        public void Add(string key, object value, TimeSpan ttl)
        {
            cache[key] = Tuple.Create(DateTimeOffset.UtcNow.Add(ttl), value);
        }

        public object Get(string productsResource)
        {
            Tuple<DateTimeOffset, object> value;
            if (cache.TryGetValue(productsResource, out value) && value.Item1 > DateTimeOffset.UtcNow)
                return value;
            cache.Remove(productsResource);
            return null;
        }
    }
}
