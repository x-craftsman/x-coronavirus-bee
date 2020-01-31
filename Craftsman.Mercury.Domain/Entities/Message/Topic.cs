using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Mercury.Domain.Entities
{
    [Table("topic")]
    public class Topic : BaseEntity
    {
        #region  Field
        /// <summary>
        /// 获取或设置Topic Name
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
        /// <summary>
        /// 获取或设置Topic 最大消息尺寸。单位：Byte
        /// </summary>
        [Column("max_message_size")]
        public int MaxMessageSize { get; set; }
        #endregion
    }
}
