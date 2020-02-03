//using Craftsman.Core.DapperExtensions;
//using Craftsman.Core.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Craftsman.Mercury.Domain.Entities
//{
//    [Table("message_record")]
//    public class MessageRecord : BaseEntity
//    {
//        /// <summary>
//        ///  获取或设置消息体
//        /// </summary>
//        [Column("body")]
//        public string Body { get; set; }
//        /// <summary>
//        /// 获取或设置消息标签
//        /// </summary>
//        [Column("tag")]
//        public string Tag { get; set; }
//        /// <summary>
//        /// 获取或设置消息元数据：Json
//        /// </summary>
//        [Column("metadata")]
//        public string Metadata { get; set; }
//        /// <summary>
//        /// 获取或设置消息状态
//        /// </summary>
//        [Column("state")]
//        public int State { get; set; }
//        /// <summary>
//        /// 获取或设置消息对应的主题Id
//        /// </summary>
//        [Column("topic_id")]
//        public int TopicId { get; set; }
//        /// <summary>
//        /// 获取或设置消息对应的主题Id
//        /// </summary>
//        public Topic Topic { get; set; }
//    }
//}
