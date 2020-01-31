using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Domain.Entities
{
    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// 基础实体类型。系统中所有的实体类均需要实现此接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// 唯一的标识用于实体类
        /// </summary>
        TPrimaryKey Id { get; set; }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an <see cref="Id"/>).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();
    }
}
