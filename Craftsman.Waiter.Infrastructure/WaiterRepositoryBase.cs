using Craftsman.Core.Domain.Entities;
using Craftsman.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Infrastructure
{
    public class WaiterRepositoryBase<TEntity, TPrimaryKey> : DapperRepositoryBase<TEntity, TPrimaryKey>
         where TEntity : class, IEntity<TPrimaryKey>
    {
    }

    public class WaiterRepositoryBase<TEntity> : WaiterRepositoryBase<TEntity, int>
         where TEntity : class, IEntity<int>
    {
    }
}
