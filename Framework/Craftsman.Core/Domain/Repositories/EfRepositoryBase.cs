using Craftsman.Core.Domain.Entities;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Runtime;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Craftsman.Core.Domain.Repositories
{
    public class EfRepositoryBase<TEntity, TPrimaryKey> : AbstractRepositoryBase<TEntity, TPrimaryKey>, IDisposable
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        #region 设计缺陷，需要从子类注入
        //TODO: 可以通过构造函数注入，但是总觉得哪里恶心～ 请原谅我是个处女作
        protected ILogger _logger;
        protected IDbResourceManager _dbResourceManager;
        protected ISession _session;

        #endregion
        public EfRepositoryBase(
            Type genericType,
            ILogger logger,
            IDbResourceManager dbResourceManager,
            ISession session)
        {
            this._dbResourceManager = dbResourceManager;
            this._logger = logger;
            this._session = session;

            var dbContext = BuildDbContextForGeneric(genericType);
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        protected DbContext BuildDbContextForGeneric(Type genericType)
        {
            //var dbContext = mamager.CreateDbContext<T>();
            DbContext dbContext = null;
            //var mamager = new DbResourceManager();
            var method = typeof(IDbResourceManager).GetMethod("GetDbContext");
            var generic = method.MakeGenericMethod(genericType);
            dbContext = generic.Invoke(_dbResourceManager, null) as DbContext;
            return dbContext;
        }
        public EfRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public override void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = _dbSet.AsQueryable().SingleOrDefault(x => x.Id.Equals(id));
            _dbSet.Remove(entity);
        }

        public override TEntity Insert(TEntity entity)
        {
            var entry = _dbSet.Add(entity);
            return entry.Entity;
        }

        public override TEntity Update(TEntity entity)
        {
            var entry = _dbSet.Update(entity);
            return entry.Entity;
        }

        public void Dispose()
        {
            if (_dbContext != null && _dbContext.ChangeTracker.HasChanges())
            {
                _dbContext.SaveChanges();
            }
        }
    }

    public class EfRepositoryBase<TEntity> : EfRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public EfRepositoryBase(
          Type genericType,
          ILogger logger,
          IDbResourceManager dbResourceManager,
          ISession session
        ) : base(genericType, logger, dbResourceManager, session) { }
    }
}
