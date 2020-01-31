using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Domain.Repositories
{
    /// <summary>
    /// A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
    }
}
