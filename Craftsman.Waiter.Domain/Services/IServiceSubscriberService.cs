using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.ObjectMapping;
using Craftsman.Core.Runtime;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.RepositoryContract;
using Craftsman.Waiter.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Craftsman.Waiter.Domain.Services
{
    public interface IServiceSubscriberService : IService
    {
        /// <summary>
        /// 获取服务订阅列表
        /// </summary>
        /// <returns>服务订阅列表</returns>
        List<ServiceSubscriber> GetServiceSubscriberList();
        ServiceSubscriber GetServiceSubscriber(int id);
        ServiceSubscriber CreateServiceSubscriber(ServiceSubscriberCreateView viewModel);
        void DeleteServiceSubscriber(int id);

        ServiceSubscriberMappingRuleDetail CreateServiceSubscriberRuleDetail(int subscriberId, int ruleId, ServiceSubscriberMappingRuleDetail detail);
        void UpdateServiceSubscriberRuleDetail(int subscriberId, int ruleId, int detailId, ServiceSubscriberMappingRuleDetail detail);
        void DeleteServiceSubscriberRuleDetail(int subscriberId, int ruleId, int detailId);

        List<ServiceSubscriberExecutionRecord> GetServiceSubscriberExecutionRecords(int subscriberId);
        List<ServiceSubscriberExecutionLog> GetServiceSubscriberExecutionLogs(int subscriberId, int recordId);

    }

    public class ServiceSubscriberService : IServiceSubscriberService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly ISession _session;
        private readonly IServiceSubscriberRepository _repoServiceSubscriber;
        private readonly IRepository<SystemService> _repoSystemService;
        private readonly IRepository<ServiceSubscriberMappingRuleDetail> _repoMappingRuleDetail;
        private readonly IRepository<ServiceSubscriberExecutionRecord> _repoExecutionRecord;
        private readonly IRepository<ServiceSubscriberExecutionLog> _repoExecutionLog;
        private readonly IRepository<Tenant> _repoTenant;
        private readonly IRepository<TenantApiKey> _repoTenantApiKey;

        private readonly ServiceSubscriberBuilder _serviceSubscriberBuilder;


        public ServiceSubscriberService(
            IObjectMapper objectMapper,
            ISession session,
            IServiceSubscriberRepository repoServiceSubscriber,
            IRepository<SystemService> repoSystemService,
            IRepository<ServiceSubscriberMappingRuleDetail> repoMappingRuleDetail,
            IRepository<ServiceSubscriberExecutionRecord> repoExecutionRecord,
            IRepository<ServiceSubscriberExecutionLog> repoExecutionLog,
            IRepository<Tenant> repoTenant,
            IRepository<TenantApiKey> repoTenantApiKey,
            ServiceSubscriberBuilder serviceSubscriberBuilder
        )
        {
            _objectMapper = objectMapper;
            _session = session;
            _repoServiceSubscriber = repoServiceSubscriber;
            _repoSystemService = repoSystemService;
            _repoMappingRuleDetail = repoMappingRuleDetail;
            _repoExecutionRecord = repoExecutionRecord;
            _repoExecutionLog = repoExecutionLog;
            _repoTenant = repoTenant;
            _repoTenantApiKey = repoTenantApiKey;
            _serviceSubscriberBuilder = serviceSubscriberBuilder;
        }
        public List<ServiceSubscriber> GetServiceSubscriberList()
        {
            return _repoServiceSubscriber.GetAll().ToList();
        }
        public ServiceSubscriber GetServiceSubscriber(int id)
        {
            var subscriber = _repoServiceSubscriber.GetServiceSubscriber(id);
            if (subscriber == null)
            {
                throw new BusinessException($"不存在Id为<{id}>的系统服务订阅！");
            }
            //TODO: 根据设置分别获取，基本信息，含日志，含明细，含分析报告... ...
            return subscriber;
        }
        public ServiceSubscriber CreateServiceSubscriber(ServiceSubscriberCreateView viewModel)
        {
            var systemService = _repoSystemService.FirstOrDefault(x => x.ActionCode == viewModel.ActionCode);
            if (systemService == null)
            {
                throw new BusinessException($"不存在对应的系统服务：ActionCode:<{viewModel.ActionCode}>");
            }

            var subscriber = viewModel.CovertToServiceSubscriber();
            subscriber.SystemServiceId = systemService.Id;
            subscriber.VerificationTenant();
            //.VerificationRule();

            subscriber = _repoServiceSubscriber.Insert(subscriber);
            return subscriber;
        }
        public void DeleteServiceSubscriber(int id)
        {
            // 验证逻辑 here...
            _repoServiceSubscriber.DeleteServiceSubscriber(id);
        }

        #region ServiceSubscriber - RuleDetail
        public ServiceSubscriberMappingRuleDetail CreateServiceSubscriberRuleDetail(int subscriberId, int ruleId, ServiceSubscriberMappingRuleDetail detail)
        {
            //验证逻辑... ...
            var subscriber = _repoServiceSubscriber.GetServiceSubscriber(subscriberId);
            if (subscriber == null)
            {
                throw new BusinessException($"不存在Id为<{subscriberId}>的系统服务订阅！");
            }
            if (subscriber.MappingRule.Id != ruleId)
            {
                throw new BusinessException($"提供的 subscriberId 和 ruleId 不匹配！");
            }

            detail.MappingRuleId = ruleId;
            detail.TenantCode = subscriber.MappingRule.TenantCode;
            detail.SetCommonFileds(_session.CurrentUser, true);

            subscriber.MappingRule.AddRuleDetail(detail);
            return _repoMappingRuleDetail.Insert(detail);
        }
        public void UpdateServiceSubscriberRuleDetail(int subscriberId, int ruleId, int detailId , ServiceSubscriberMappingRuleDetail detail)
        {
            //验证逻辑... ...
            var subscriber = GetServiceSubscriber(subscriberId);
            if (subscriber.MappingRule.Id != ruleId)
            {
                throw new BusinessException($"提供的 subscriberId 和 ruleId 不匹配！");
            }
            var legacyDetail = subscriber.MappingRule.Details.SingleOrDefault(x => x.Id == detailId);
            if (legacyDetail == null)
            {
                throw new BusinessException($"不存在Id为<{detailId}>的明细信息！");
            }

            // 需要进一步封装
            // TODO: var detail = legacyDetail.Clone();

            var d = _objectMapper.Map<ServiceSubscriberMappingRuleDetail>(legacyDetail);
            legacyDetail.Source = detail.Source;
            legacyDetail.Target = detail.Target;
            legacyDetail.SetCommonFileds(_session.CurrentUser);

            subscriber.MappingRule.UpdateRuleDetail(legacyDetail);
            _repoMappingRuleDetail.Update(legacyDetail);
        }
        public void DeleteServiceSubscriberRuleDetail(int subscriberId, int ruleId, int detailId)
        {
            //验证逻辑... ...
            var subscriber = GetServiceSubscriber(subscriberId);
            if (subscriber.MappingRule.Id != ruleId)
            {
                throw new BusinessException($"提供的 subscriberId 和 ruleId 不匹配！");
            }
            subscriber.MappingRule.DeleteRuleDetail(detailId);
            _repoMappingRuleDetail.Delete(detailId);
        }
        #endregion ServiceSubscriber - RuleDetail

        #region ServiceSubscriber - ExecutionRecord
        public List<ServiceSubscriberExecutionRecord> GetServiceSubscriberExecutionRecords(int subscriberId)
        {
            // GetServiceSubscriber(subscriberId);
            var subscriber = _repoServiceSubscriber.GetServiceSubscriber(subscriberId);
            if (subscriber == null)
            {
                throw new BusinessException($"不存在Id为<{subscriberId}>的系统服务订阅！");
            }
            return this._repoExecutionRecord.GetAllList(x => x.SubscriberId == subscriberId);
        }
        public List<ServiceSubscriberExecutionLog> GetServiceSubscriberExecutionLogs(int subscriberId, int recordId)
        {
            // GetServiceSubscriber(subscriberId);
            var subscriber = _repoServiceSubscriber.GetServiceSubscriber(subscriberId);
            if (subscriber == null)
            {
                throw new BusinessException($"不存在Id为<{subscriberId}>的系统服务订阅！");
            }
            return this._repoExecutionLog.GetAllList(x => x.RecordId == recordId);
        }
        #endregion ServiceSubscriber - ExecutionRecord


        public ServiceSubscriber BuildServiceSubscriber(ServiceSubscriberCreateView viewModel)
        {

            var systemService = _repoSystemService.FirstOrDefault(x => x.ActionCode == viewModel.ActionCode);
            if (systemService == null)
            {
                throw new BusinessException($"不存在对应的系统服务：ActionCode:<{viewModel.ActionCode}>");
            }

            var tenant = _repoTenant.FirstOrDefault(x => x.Code == viewModel.TenantCode);
            tenant.ApiKeys = _repoTenantApiKey.GetAllList(x => x.TenantId == tenant.Id);
            
            var tempSubscriber = viewModel.CovertToServiceSubscriber();

            var subscriber = _serviceSubscriberBuilder
                .SetTenant(tenant)
                .SetSystemService(systemService)
                .SetMappingLogic(null, null)
                .Build();

            subscriber = _repoServiceSubscriber.Insert(subscriber);
            return subscriber;
        }

    }
}
