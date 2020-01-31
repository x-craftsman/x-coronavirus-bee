using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime.HardCode
{
    /// <summary>
    /// HardCode Session 用户框架初期调试（后期会用户单元测试）
    /// </summary>
    public class HardCodeSession : ISession
    {
        public Guid Id { get; set; }
        public ICurrentUser CurrentUser { get; set; }
        public ITenant Tenant { get; set; }

        public HardCodeSession()
        {
            this.Id = Guid.NewGuid();
            this.CurrentUser = new HardCodeCurrentUser();
            this.Tenant = new HardCodeTenant();
        }
    }
}
