using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.AuthCenter
{
    /// <summary>
    /// 获取用户权限接口
    /// </summary>
    public interface IUserAuthorizationCreator
    {
        //Dictionary<int, string> GetUserAuthorization(AuthUserContext context);
        Dictionary<int, string> GetUserAuthorization(int userId);
    }
}
