using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime.Caching
{
    public class DemoCacheManager : ICacheManager
    {
        public void Dispose()
        {
        }

        public TValue GetRuntimeDataCache<TKey, TValue>(string name, TimeSpan expireTime, CacheExpireTimeType type, Func<TKey, TValue> factory)
        {
            return factory(default);
        }

        public TValue GetRuntimeDataCache<TKey, TValue>(string name, TimeSpan expireTime, Func<TKey, TValue> factory)
        {
            return GetRuntimeDataCache(name, expireTime, CacheExpireTimeType.Absolute, factory);
        }

        public TValue GetRuntimeDataCache<TKey, TValue>(string name, Func<TKey, TValue> factory)
        {
            return GetRuntimeDataCache(name, TimeSpan.FromMinutes(30), factory);
        }


        public TValue GetSystemSettingCache<TKey, TValue>(string name)
        {
            //throw new NotImplementedException();
            return default;
        }
    }
}
