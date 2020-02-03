using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Config
{
    public class HardCodeConfigManager : IConfigManager
    {
        public DatabaseInfo Database { get; protected set; }
        public RedisInfo Redis { get; protected set; }
        public KafkaInfo Kafka { get; protected set; }

        public FileServer FileServer { get; protected set; }

        public HardCodeConfigManager()
        {
            Database = new DatabaseInfo()
            {
                DataSource = "47.98.161.28",
                Port = 3306,
                InitialCatalog = "x_nCoV",
                UserId = "root",
                Password = "Craftsman@2020"
            };

            Redis = new RedisInfo()
            {
                Host = "192.168.100.20",
                Port = 6379
            };

            Kafka = new KafkaInfo()
            {
                Host = "192.168.100.20",
                Port = 9092
            };

            FileServer = new FileServer()
            {
                Host = "192.168.100.172",
                Port = 22,
                UserName= "testuser",
                Password= "1q2w3e!@#",
                RootDirectory = "/home/testuser"
            };
        }
    }
}
