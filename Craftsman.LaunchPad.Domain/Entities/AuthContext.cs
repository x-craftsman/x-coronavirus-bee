using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.Entities
{
    /// <summary>
    /// 认证上下文对象
    /// </summary>
    public class AuthContext
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
