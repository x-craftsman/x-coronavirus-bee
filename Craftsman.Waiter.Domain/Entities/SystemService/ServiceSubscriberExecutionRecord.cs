using Craftsman.Core.DapperExtensions;
using System.Collections.Generic;

namespace Craftsman.Waiter.Domain.Entities
{
    /// <summary>
    /// 系统服务定于查询日志
    /// </summary>
    [Table("service_subscriber_execution_record")]
    public class ServiceSubscriberExecutionRecord : BaseEntity
    {
        [Column("subscriber_id")]
        public int SubscriberId { get; set; }
        [Column("action_code")]
        public string ActionCode { get; set; }
        [Column("system_code")]
        public string SystemCode { get; set; }
        [Column("status")]
        public int Status { get; set; }
        [Column("start_time")]
        public System.DateTime StartTime { get; set; }
        [Column("end_time")]
        public System.DateTime EndTime { get; set; }
        [Column("tenant_code")]
        public string TenantCode { get; set; }
        [NotMapped]
        public List<ServiceSubscriberExecutionLog> ExecutionLogs { get; set; }
    }
}

