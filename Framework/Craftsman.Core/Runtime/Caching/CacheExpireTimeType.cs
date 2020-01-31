using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime.Caching
{
    /// <summary>
    /// 缓存过期类型
    /// </summary>
    public enum CacheExpireTimeType
    {
        /// <summary>
        /// 滑动过期
        /// </summary>
        Sliding,
        /// <summary>
        /// 绝对过期
        /// </summary>
        Absolute,
        /// <summary>
        /// 永不过期
        /// </summary>
        NeverExpire
    }
}
