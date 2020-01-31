using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Domain.Repositories
{
    public interface IUnitOfWork
    {
        void RegisterAmended(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository);
        void RegisterNew(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository);
        void RegisterRemoved(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository);
        void Commit();
    }
}
