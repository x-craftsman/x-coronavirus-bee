using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Domain.Entities
{
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity
    {
    }

    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
    {

    }
}
