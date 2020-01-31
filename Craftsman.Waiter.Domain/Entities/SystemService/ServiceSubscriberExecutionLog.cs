using Craftsman.Core.DapperExtensions;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("service_subscriber_execution_log")]
    public class ServiceSubscriberExecutionLog : BaseEntity
    {
        [Column("record_id")]
        public int RecordId { get; set; }
        [Column("tenant_code")]
        public string TenantCode { get; set; }
        [Column("action_code")]
        public string ActionCode { get; set; }
        [Column("system_code")]
        public string SystemCode { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("content")]
        public string Content { get; set; }
    }
}

