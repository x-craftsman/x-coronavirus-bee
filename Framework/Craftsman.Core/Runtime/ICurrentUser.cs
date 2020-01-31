using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime
{
    /// <summary>
    /// 当前登录用户
    /// </summary>
    public interface ICurrentUser
    {
        int Id { get; }
        string Name { get; }
    }
}
