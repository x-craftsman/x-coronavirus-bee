using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.Waiter.WebUI.CustomFilter
{
    /// <summary>
    /// 没有权限验证
    /// </summary>
    public class HangfireNoAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
