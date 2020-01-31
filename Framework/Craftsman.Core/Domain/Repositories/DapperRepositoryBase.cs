﻿using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Craftsman.Core.Domain.Repositories
{
    public class DapperRepositoryBase<TEntity, TPrimaryKey> : AbstractRepositoryBase<TEntity, TPrimaryKey>, IDisposable
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public Guid Guid = Guid.NewGuid();
        protected ILogger _logger;
        protected ISession _session;
        protected IDbConnection _dbConnection;

        public DapperRepositoryBase() { }
        public DapperRepositoryBase(
            ILogger logger,
            IDbResourceManager dbResourceManager,
            ISession session
            )
        {
            this._logger = logger;
            this._session = session;
            this._dbConnection = dbResourceManager.CurrentDbConnection;
        }
        public override IQueryable<TEntity> GetAll()
        {
            CheckDbConnection();
            OpenDbConnection();

            var entitys = _dbConnection.GetList<TEntity>().AsQueryable();
            CloseDbConnection();
            return entitys;
        }
        public override TEntity Get(TPrimaryKey id)
        {
            //TODO: 后期需要使用Command 进行进一步封装，实现UnitOfWork
            CheckDbConnection();

            OpenDbConnection();
            var entity = _dbConnection.Get<TEntity>(id);
            CloseDbConnection();

            return entity;
        }

        public override TEntity Insert(TEntity entity)
        {
            //TODO: 后期需要使用Command 进行进一步封装，实现UnitOfWork
            CheckDbConnection();

            OpenDbConnection();
            var key = _dbConnection.Insert(entity);
            var realEntity = _dbConnection.Get<TEntity>(key);
            CloseDbConnection();

            return realEntity;
        }

        public override TEntity Update(TEntity entity)
        {
            CheckDbConnection();
            OpenDbConnection();
            _dbConnection.Update(entity);
            var realEntity = _dbConnection.Get<TEntity>(entity.Id);
            CloseDbConnection();

            return realEntity;
        }
        public override void Delete(TEntity entity)
        {
            CheckDbConnection();
            OpenDbConnection();
            var id = entity.Id;
            var entityForDelete = _dbConnection.Get<TEntity>(id);
            if (entityForDelete == null)
            {
                throw new Exception($"[DapperRepositoryBase]:不存在主键为<{id}>的数据!");
            }
            _dbConnection.Delete<TEntity>(id);
            CloseDbConnection();
        }

        public override void Delete(TPrimaryKey id)
        {
            CheckDbConnection();
            OpenDbConnection();
            var entity = _dbConnection.Get<TEntity>(id);
            if (entity == null)
            {
                throw new Exception($"[DapperRepositoryBase]:不存在主键为<{id}>的数据!");
            }
            _dbConnection.Delete<TEntity>(id);
            CloseDbConnection();
        }

        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
            }
        }

        protected void CheckDbConnection()
        {
            if (_dbConnection == null)
            {
                throw new Exception("[DapperRepositoryBase]: 未初始化的 _dbConnection!");
            }
        }

        protected void OpenDbConnection()
        {
            if (_dbConnection.State == ConnectionState.Closed) _dbConnection.Open();
        }
        protected void CloseDbConnection()
        {
            if (_dbConnection.State != ConnectionState.Closed) _dbConnection.Close();
        }

    }

    public class DapperRepositoryBase<TEntity> : DapperRepositoryBase<TEntity, int>, IRepository<TEntity>
       where TEntity : class, IEntity<int>
    {
        public DapperRepositoryBase(
          ILogger logger,
          IDbResourceManager dbResourceManager,
          ISession session
        ) : base(logger, dbResourceManager, session) { }

    }
}
