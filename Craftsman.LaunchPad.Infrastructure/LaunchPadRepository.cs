using Craftsman.Core.Domain.Entities;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Infrastructure
{
    public class LaunchPadRepository<TEntity, TPrimaryKey> : EfRepositoryBase<TEntity, TPrimaryKey>
     where TEntity : class, IEntity<TPrimaryKey>
    {
        public LaunchPadRepository(
            ILogger logger,
            IDbResourceManager dbResourceManager,
            ISession session
        ) : base(typeof(LaunchPadContext), logger, dbResourceManager, session) { }
    }

    public class LaunchPadRepository<TEntity> : EfRepositoryBase<TEntity>
        where TEntity : class, IEntity<int>
    {
        public LaunchPadRepository(
            ILogger logger,
            IDbResourceManager dbResourceManager,
            ISession session
        ) : base(typeof(LaunchPadContext), logger, dbResourceManager, session) { }

    }
}
