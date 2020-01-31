using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.AuthCenter
{
    /// <summary>
    /// 获取用户信息接口
    /// </summary>
    public interface IUserInfomationCreator
    {
        //CurrentUserDM GetUserInformation(AuthUserContext context);
        ICurrentUser GetUserInformation(int context);
    }
}
