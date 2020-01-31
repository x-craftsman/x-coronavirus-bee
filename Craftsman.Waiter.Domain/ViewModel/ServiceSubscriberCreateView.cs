using Craftsman.Core.CustomizeException;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.SysEnum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Craftsman.Waiter.Domain.ViewModel
{
    public class ServiceSubscriberCreateView
    {
        public string ActionCode { get; set; }
        public string TenantCode { get; set; }
        public int ApiKeyId { get; set; }
        public ServiceSubscriberType SubscriberType { get; set; }
        public MappingRuleType RuleType { get; set; }
        public AvailableState State { get; set; }
        public List<ServiceSubscriberDetailCreateView> RuleDetails { get; set; }

        public ServiceSubscriber CovertToServiceSubscriber()
        {
            if (this.RuleDetails == null || this.RuleDetails.Count == 0)
            {
                throw new BusinessException("规则明细不能为空！");
            }

            //build rule
            var mappingRule = new ServiceSubscriberMappingRule();
            mappingRule.Type = this.RuleType;
            mappingRule.TenantCode = this.TenantCode;
            mappingRule.Details = this.RuleDetails
                .Select(detail => new ServiceSubscriberMappingRuleDetail { Source = detail.Source, Target = detail.Target, TenantCode = this.TenantCode })
                .ToList();

            //build subscriber
            var subscriber = new ServiceSubscriber();
            subscriber.Type = this.SubscriberType;
            subscriber.Status = this.State;
            subscriber.TenantCode = this.TenantCode;
            subscriber.ApiKeyId = this.ApiKeyId;
            subscriber.MappingRule = mappingRule;
            return subscriber;
        }
    }
}
