using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime.Caching
{
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// 获取缓存（如果没有会创建）
        /// </summary>
        /// <typeparam name="TKey">缓存对象的Key</typeparam>
        /// <typeparam name="TValue">缓存对象的Value</typeparam>
        /// <param name="name">缓存名称</param>
        /// <param name="expireTime">过期时间</param>
        /// <param name="type">过期类型</param>
        /// <param name="factory">创建缓存的工厂方法</param>
        /// <returns></returns>
        TValue GetRuntimeDataCache<TKey, TValue>(string name, TimeSpan expireTime, CacheExpireTimeType type, Func<TKey, TValue> factory);
        TValue GetRuntimeDataCache<TKey, TValue>(string name, TimeSpan expireTime, Func<TKey, TValue> factory);
        TValue GetRuntimeDataCache<TKey, TValue>(string name, Func<TKey, TValue> factory);
        //TValue GetRuntimeDataCache<TKey, TValue>(TimeSpan expireTime, CacheExpireTimeType type, Func<TKey, TValue> factory);
        //TValue GetRuntimeDataCache<TKey, TValue>(TimeSpan expireTime, Func<TKey, TValue> factory);
        //TValue GetRuntimeDataCache<TKey, TValue>(Func<TKey, TValue> factory);

        TValue GetSystemSettingCache<TKey, TValue>(string name);
    }

}
