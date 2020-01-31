using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Mercury.Domain.Entities
{
    [Table("subscription")]
    public class Subscription : BaseEntity
    {
        /// <summary>
        ///  获取或设置订阅名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置endpoint
        /// </summary>
        [Column("endpoint")]
        public string Endpoint { get; set; }
        /// <summary>
        /// 获取或设置过滤标记
        /// </summary>
        [Column("filter_tag")]
        public string FilterTag { get; set; }
        /// <summary>
        /// 获取或设置处理策略
        /// </summary>
        [Column("notify_strategy")]
        public string NotifyStrategy { get; set; }
        /// <summary>
        /// 获取或设置消息状态
        /// </summary>
        [Column("state")]
        public int State { get; set; }
        /// <summary>
        /// 获取或设置消息对应的主题Id
        /// </summary>
        [Column("topic_id")]
        public int TopicId { get; set; }
        /// <summary>
        /// 获取或设置消息对应的主题Id
        /// </summary>
        public Topic Topic { get; set; }
    }
}
