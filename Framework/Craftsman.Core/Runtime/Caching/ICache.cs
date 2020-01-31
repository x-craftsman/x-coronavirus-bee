using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime.Caching
{
    /// <summary>
    /// Defines a cache that can be store and get items by keys.（定义缓存保存Key & Value）
    /// </summary>
    public interface ICache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Unique name of the cache.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Default sliding expire time of cache items.（定义滑动的缓存过期时间）
        /// Default value: 60 minutes (1 hour).
        /// Can be changed by configuration.
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Default absolute expire time of cache items.（定义固定的缓存过期时间）
        /// Default value: null (not used).
        /// </summary>
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        /// <summary>
        /// Gets an item from the cache.（从缓存获取数据）
        /// This method hides cache provider failures (and logs them),
        /// uses the factory method to get the object if cache provider fails.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="factory">Factory method to create cache item if not exists</param>
        /// <returns>Cached item</returns>
        TValue Get(TKey key, Func<TKey, TValue> factory);

        /// <summary>
        /// Gets items from the cache.
        /// This method hides cache provider failures (and logs them),
        /// uses the factory method to get the object if cache provider fails.
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <param name="factory">Factory method to create cache item if not exists</param>
        /// <returns>Cached item</returns>
        TValue[] Get(TKey[] keys, Func<TKey, TValue> factory);


        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Cached item or null if not found</returns>
        TValue GetOrDefault(TKey key);

        /// <summary>
        /// Gets items from the cache. For every key that is not found, a null value is returned.
        /// </summary>
        /// <param name="keys">Keys</param>
        /// <returns>Cached items</returns>
        TValue[] GetOrDefault(TKey[] keys);

      
        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// Use one of the expire times at most (<paramref name="slidingExpireTime"/> or <paramref name="absoluteExpireTime"/>).
        /// If none of them is specified, then
        /// <see cref="DefaultAbsoluteExpireTime"/> will be used if it's not null. Othewise, <see cref="DefaultSlidingExpireTime"/>
        /// will be used.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        /// <param name="absoluteExpireTime">Absolute expire time</param>
        void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// Saves/Overrides items in the cache by the pairs.
        /// Use one of the expire times at most (<paramref name="slidingExpireTime"/> or <paramref name="absoluteExpireTime"/>).
        /// If none of them is specified, then
        /// <see cref="DefaultAbsoluteExpireTime"/> will be used if it's not null. Othewise, <see cref="DefaultSlidingExpireTime"/>
        /// will be used.
        /// </summary>
        /// <param name="pairs">Pairs</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        /// <param name="absoluteExpireTime">Absolute expire time</param>
        void Set(KeyValuePair<TKey, TValue>[] pairs, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null);

        /// <summary>
        /// Removes a cache item by it's key (does nothing if given key does not exists in the cache).
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(TKey key);

        /// <summary>
        /// Removes cache items by their keys.
        /// </summary>
        /// <param name="keys">Keys</param>
        void Remove(TKey[] keys);

        /// <summary>
        /// Clears all items in this cache.
        /// </summary>
        void Clear();
    }

    public interface ICache : ICache<string, object> { }
}
