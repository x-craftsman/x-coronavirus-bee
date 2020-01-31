using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using Craftsman.Waiter.Domain.SysEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("service_subscriber")]
    public class ServiceSubscriber : BaseAggregateRoot
    {
        #region Property
        [Column("system_service_id")]
        public int SystemServiceId { get; set; }
        [Column("apikey_id")]
        public int ApiKeyId { get; set; }
        [Column("type")]
        public ServiceSubscriberType Type { get; set; }
        [Column("status")]
        public AvailableState Status { get; set; }
        [Column("tenant_code")]
        public string TenantCode { get; set; }

        [NotMapped]
        public SystemService SystemService { get; set; }
        [NotMapped]
        public ServiceSubscriberMappingRule MappingRule { get; set; }
        public TenantApiKey ApiKey { get; set; }
        [NotMapped]
        public List<ServiceSubscriberCustomLogic> CustomLogic { get; set; }

        [NotMapped]
        public List<ServiceSubscriberExecutionRecord> ExecutionRecords { get; set; }
        #endregion  Property

        #region Action
        /// <summary>
        /// 构建订阅流程
        /// </summary>
        public void BuildServiceSubscriber() { }

        public void ExecuteServiceSubscriber() { }

        public void DisableServiceSubscriber() { }

        #endregion Action
        public ServiceSubscriber VerificationTenant()
        { return this; }
    }
}
