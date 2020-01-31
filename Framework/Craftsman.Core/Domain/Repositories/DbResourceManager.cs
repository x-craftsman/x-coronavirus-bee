using Craftsman.Core.Dependency;
using Craftsman.Core.Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Craftsman.Core.Domain.Repositories
{
    [Obsolete("功能移至IConfigManager，请尽快使用 IConfigManager 替代")]
    public class DbResourceManager : IDbResourceManager
    {
        public Guid Guid = Guid.NewGuid();

        private DbContext _dbContext;
        private DbConnection _dbConnection;

        private object _lockObject = new object();

        public IEnumerator<DbConnection> AllDbConnections { get { return GetAllDbConnections(); } }

        public DbConnection CurrentDbConnection { get { return GetDbConnection(); } }

        public IConfigManager ConfigManager { get; set; }

        public virtual DbConnection GetDbConnection()
        {
            if (_dbConnection == null)
            {
                lock (_lockObject)
                {
                    if (_dbConnection == null) { _dbConnection = CreateDbConnection(); }
                }
            }
            return _dbConnection;
        }

        protected virtual DbContext CreateDbContext<TContext>() where TContext : DbContext
        {
            //TODO: 需要重构，Hard code 需要支持多库。
            var conn = GetDbConnection();
            DbContextOptionsBuilder<TContext> builder = new DbContextOptionsBuilder<TContext>();
            builder.UseMySql(conn);
            var dbContext = (TContext)Activator.CreateInstance(typeof(TContext), builder.Options);
            return dbContext;
        }

        public virtual DbContext GetDbContext<TContext>() where TContext : DbContext
        {
            if (_dbContext == null)
            {
                lock (_lockObject)
                {
                    if (_dbContext == null) { _dbContext = CreateDbContext<TContext>(); }
                }
            }
            return _dbContext;
        }

        #region TODO
        protected virtual IEnumerator<DbConnection> GetAllDbConnections()
        {
            throw new NotImplementedException();
        }

        protected virtual DbConnection CreateDbConnection(/*TODO: Dialect dialect*/)
        {
            //数据库连接
            var strConn = ConfigManager.Database.ConvertToConnectionString();
            var conn = new MySqlConnection(strConn);
            return conn;
        }
        #endregion TODO
    }
}
