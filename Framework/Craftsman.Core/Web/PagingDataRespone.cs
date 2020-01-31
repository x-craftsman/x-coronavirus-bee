using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Web
{
    /// <summary>
    /// 分页响应数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingDataRespone<T>
    {
        /// <summary>
        /// 获取或设置返回数据
        /// </summary>
        public IList<T> Data { get; set; }
        /// <summary>
        /// 获取或设置分页信息
        /// </summary>
        public Pagination Pagination { get; set; }
    }
}
