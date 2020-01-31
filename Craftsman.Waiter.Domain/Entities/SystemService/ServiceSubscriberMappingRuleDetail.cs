using Craftsman.Core.DapperExtensions;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("service_subscriber_mapping_rule_detail")]
    public class ServiceSubscriberMappingRuleDetail : BaseEntity
    {
        [Column("mapping_rule_id")]
        public int MappingRuleId { get; set; }
        [Column("source")]
        public string Source { get; set; }
        [Column("target")]
        public string Target { get; set; }
        [Column("tenant_code")]
        public string TenantCode { get; set; }
    }
}

