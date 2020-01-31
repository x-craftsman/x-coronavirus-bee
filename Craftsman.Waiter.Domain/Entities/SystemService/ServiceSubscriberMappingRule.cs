using Craftsman.Core.DapperExtensions;
using Craftsman.Waiter.Domain.SysEnum;
using System.Collections.Generic;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("service_subscriber_mapping_rule")]
    public class ServiceSubscriberMappingRule : BaseEntity
    {
        [Column("subscriber_id")]
        public int SubscriberId { get; set; }
        [Column("type")]
        public MappingRuleType Type { get; set; }
        [Column("tenant_code")]
        public string TenantCode { get; set; }
        [NotMapped]
        public List<ServiceSubscriberMappingRuleDetail> Details { get; set; }


        public ServiceSubscriberMappingRule VerificationRule()
        { return this; }
        public ServiceSubscriberMappingRule AddRuleDetail(ServiceSubscriberMappingRuleDetail detail)
        {
            // Do delete logic.

            // verification rule.
            VerificationRule();
            return this;
        }
        public ServiceSubscriberMappingRule UpdateRuleDetail(ServiceSubscriberMappingRuleDetail detail)
        {
            // Do delete logic.

            // verification rule.
            VerificationRule();
            return this;
        }
        public ServiceSubscriberMappingRule DeleteRuleDetail(int detailId)
        {
            // Do delete logic.

            // verification rule.
            VerificationRule();
            return this;
        }
    }
}

