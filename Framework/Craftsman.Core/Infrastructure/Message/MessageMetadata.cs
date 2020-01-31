using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Message
{
    /// <summary>
    /// 消息描述元数据
    /// </summary>
    public class MessageMetadata
    {
        public string ActionCode { get; set; }
        public string TenantCode { get; set; }
    }
}
