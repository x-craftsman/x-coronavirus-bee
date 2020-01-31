using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.AuthCenter
{
    /// <summary>
    /// 用户认证接口
    /// </summary>
    public interface IUserAuthenticationCreator
    {
        /// <summary>
        /// Authentication User (use to sign in logic.)
        /// </summary>
        /// <param name="loginName">登录名称</param>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        ICurrentUser AuthenticationUser(string loginName, string password);
    }
}
