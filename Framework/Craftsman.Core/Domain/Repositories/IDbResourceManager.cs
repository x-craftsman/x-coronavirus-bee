using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Craftsman.Core.Domain.Repositories
{
    [Obsolete("功能移至IConfigManager，请尽快使用 IConfigManager 替代")]
    public interface IDbResourceManager
    {
        IEnumerator<DbConnection> AllDbConnections { get; }
        DbConnection CurrentDbConnection { get; }
        DbContext GetDbContext<TContext>() where TContext : DbContext;
    }
}
