using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Config
{
    /// <summary>
    /// 配置管理接口
    /// </summary>
    public interface IConfigManager
    {
        DatabaseInfo Database { get; }
        RedisInfo Redis { get; }
        KafkaInfo Kafka { get; }
        FileServer FileServer { get; }
    }

    public class DatabaseInfo
    {
        public string DataSource { get; set; }
        public int Port { get; set; }
        public string InitialCatalog { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }

        public string ConvertToConnectionString(string dbtype = "mysql")
        {
            var strConn = string.Empty;
            switch (dbtype)
            {
                default:
                case "mysql":
                    strConn = $"Data Source={this.DataSource};port={(this.Port > 0 ? this.Port : 3306)};Initial Catalog={this.InitialCatalog};user id={UserId};password={Password};Character Set=utf8;pooling=true;MaximumPoolsize=100;";
                    break;
            }
            return strConn;
        }
    }

    public class RedisInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
        /// <summary>
        /// 暂不使用
        /// </summary>
        public string Auth { get; set; }
    }

    public class KafkaInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string BootstrapServers {
            get
            {
                return $"{Host}:{Port}";
            }
        }
    }

    public class FileServer
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RootDirectory { get; set; }
    }
}
