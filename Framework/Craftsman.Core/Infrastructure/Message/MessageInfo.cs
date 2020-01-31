using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Message
{
    public class MessageInfo<T>
    {
        public T Body { get; set; }
        public string Tag { get; set; }
        public MessageMetadata Metadata { get; set; }
    }

    public class MessageInfo : MessageInfo<dynamic> { }
}

