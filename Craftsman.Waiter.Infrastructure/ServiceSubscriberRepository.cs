using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.ObjectMapping;
using Craftsman.Core.Runtime;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.RepositoryContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Craftsman.Waiter.Infrastructure
{
    // public class ServiceSubscriberRepository : WaiterRepositoryBase<ServiceSubscriber, int>, IServiceSubscriberRepository
    public class ServiceSubscriberRepository : IServiceSubscriberRepository
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ISession _session;

        #region Repositories
        private readonly IRepository<ServiceSubscriber> _repoServiceSubscriber;
        private readonly IRepository<SystemService> _repoSystemService;
        private readonly IRepository<ServiceSubscriberMappingRule> _repoMappingRule;
        private readonly IRepository<ServiceSubscriberMappingRuleDetail> _repoMappingRuleDetail;
        private readonly IRepository<ServiceSubscriberExecutionLog> _repoExecutionLog;
        private readonly IRepository<ServiceSubscriberExecutionRecord> _repoExecutionRecord;
        private readonly IRepository<TenantApiKey> _repoTenantApiKey;
        private readonly IRepository<Tenant> _repoTenant;
        #endregion

        public ServiceSubscriberRepository(
            IObjectMapper objectMapper,
            ISession session,
            IRepository<ServiceSubscriber> repoServiceSubscriber,
            IRepository<SystemService> repoSystemService,
            IRepository<ServiceSubscriberMappingRule> repoMappingRule,
            IRepository<ServiceSubscriberMappingRuleDetail> repoMappingRuleDetail,
            IRepository<ServiceSubscriberExecutionLog> repoExecutionLog,
            IRepository<ServiceSubscriberExecutionRecord> repoExecutionRecord,
            IRepository<TenantApiKey> repoTenantApiKey,
            IRepository<Tenant> repoTenant
        )
        {
            _objectMapper = objectMapper;
            _session = session;
            _repoServiceSubscriber = repoServiceSubscriber;
            _repoSystemService = repoSystemService;
            _repoExecutionLog = repoExecutionLog;
            _repoExecutionRecord = repoExecutionRecord;
            _repoMappingRule = repoMappingRule;
            _repoMappingRuleDetail = repoMappingRuleDetail;
            _repoTenantApiKey = repoTenantApiKey;
            _repoTenant = repoTenant;
        }

        public ServiceSubscriber Insert(ServiceSubscriber entity)
        {
            //Insert ServiceSubscriber
            entity.SetCommonFileds(_session.CurrentUser, true);
            entity.MappingRule.SetCommonFileds(_session.CurrentUser, true);

            entity.Id = _repoServiceSubscriber.InsertAndGetId(entity);
            entity.MappingRule.SubscriberId = entity.Id;
            entity.MappingRule.Id = _repoMappingRule.InsertAndGetId(entity.MappingRule);
            foreach (var detail in entity.MappingRule.Details)
            {
                detail.MappingRuleId = entity.MappingRule.Id;
                detail.SetCommonFileds(_session.CurrentUser, true);
                _repoMappingRuleDetail.Insert(detail);
            }

            return entity;
        }

        public IQueryable<ServiceSubscriber> GetAll()
        {
            var subscribers = new List<ServiceSubscriber>();

            var subscriberDtos = _repoServiceSubscriber.GetAllList();
            // 存在性能问题， 后续需要改进
            foreach (var dto in subscriberDtos)
            {
                var service = _repoSystemService.Single(x => x.Id == dto.SystemServiceId);
                var entity = _objectMapper.Map<ServiceSubscriber>(dto);
                entity.SystemService = _objectMapper.Map<SystemService>(service);
                subscribers.Add(entity);
            }

            return subscribers.AsQueryable();
        }

        public ServiceSubscriber GetServiceSubscriber(string actionCode, string tenantCode)
        {
            //TODO: 此处需要使用 smart-sql 改造。目前的实现方式 性能不好！！！
            var systemServices = _repoSystemService.GetAllList(x => x.ActionCode == actionCode);
            var serviceSubscribers = _repoServiceSubscriber.GetAllList(x => x.TenantCode == tenantCode);

            ServiceSubscriber serviceSubscriber = null;
            foreach (var service in systemServices)
            {
                foreach (var subscriber in serviceSubscribers)
                {
                    if (subscriber.SystemServiceId == service.Id)
                    {
                        serviceSubscriber = _objectMapper.Map<ServiceSubscriber>(subscriber);
                        serviceSubscriber.SystemService = service;
                        break;
                    }
                }
                if (serviceSubscriber != null) break;
            }
            if (serviceSubscriber == null) return null;

            //构造数据
            #region 获取 mapping-rule
            var mappingRule =  _repoMappingRule.Single(x => x.SubscriberId == serviceSubscriber.Id);
            mappingRule.Details = _repoMappingRuleDetail.GetAllList(x => x.MappingRuleId == mappingRule.Id);
            serviceSubscriber.MappingRule = mappingRule;
            #endregion 获取 mapping-rule

            serviceSubscriber.ApiKey = _repoTenantApiKey.Get(serviceSubscriber.ApiKeyId);
            return serviceSubscriber;
        }

        public ServiceSubscriber GetServiceSubscriber(int id)
        {
            var subscriber = _repoServiceSubscriber.Get(id);
            subscriber.SystemService = _repoSystemService.Get(subscriber.SystemServiceId);
            subscriber.ApiKey = _repoTenantApiKey.Get(subscriber.ApiKeyId);
            subscriber.ApiKey.Tenant = _repoTenant.Single(x => x.Id == subscriber.ApiKey.TenantId);
            subscriber.MappingRule = _repoMappingRule.Single(x => x.SubscriberId == subscriber.Id);
            subscriber.MappingRule.Details = _repoMappingRuleDetail.GetAllList(x => x.MappingRuleId == subscriber.MappingRule.Id);
            return subscriber;
        }

        public void DeleteServiceSubscriber(int id)
        {
            var subscriber = _repoServiceSubscriber.Get(id);
            if (subscriber == null)
            {
                throw new BusinessException($"[ServiceSubscriberRepository]:不存在ID为：<{id}>的订阅信息！");
            }
            var rule = _repoMappingRule.FirstOrDefault(x => x.SubscriberId == subscriber.Id);

            _repoMappingRuleDetail.Delete(x => x.MappingRuleId == rule.Id);
            _repoMappingRule.Delete(x => x.SubscriberId == subscriber.Id);
            _repoServiceSubscriber.Delete(id);
        }

    }
}
