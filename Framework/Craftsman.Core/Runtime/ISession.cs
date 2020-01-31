using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Runtime
{
    public interface ISession
    {
        Guid Id { get; set; }
        ICurrentUser CurrentUser { get; set; }
        ITenant Tenant { get; set; }
    }

}
