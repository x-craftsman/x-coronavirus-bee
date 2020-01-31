using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Domain.Repositories
{
    public interface IUnitOfWorkRepository
    {
        void PersistCreationOf(IAggregateRoot entity);
        void PersistUpdateOf(IAggregateRoot entity);
        void PersistDeletionOf(IAggregateRoot entity);
    }
}
