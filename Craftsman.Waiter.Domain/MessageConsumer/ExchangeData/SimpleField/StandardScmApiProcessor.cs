using System;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Text;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Waiter.Domain.Entities;
using System.Net;
using System.Linq;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Core.Runtime.Caching;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    public class StandardScmApiProcessor : IProcessor
    {
        private ILogger _logger;
        private ICacheManager _cacheManager;
        private IRepository<SystemService> _repoSystemService;
        private IRepository<SystemServiceAuthConfig> _repoSystemServiceAuthConfig;
        private IRepository<Tenant> _repoTenant;
        private IRepository<TenantApiKey> _repoTenantApiKey;
        private IRepository<ServiceSubscriber> _repoServiceSubscriber;

        public StandardScmApiProcessor(
            ILogger logger,
            ICacheManager cacheManager,
            IRepository<SystemService> repoSystemService,
            IRepository<Tenant> repoTenant,
            IRepository<TenantApiKey> repoTenantApiKey,
            IRepository<SystemServiceAuthConfig> repoSystemServiceAuthConfig, //,//ICache cache
            IRepository<ServiceSubscriber> repoServiceSubscriber
        )
        {
            this._logger = logger;
            this._cacheManager = cacheManager;
            this._repoSystemService = repoSystemService;
            this._repoTenant = repoTenant;
            this._repoTenantApiKey = repoTenantApiKey;
            this._repoSystemServiceAuthConfig = repoSystemServiceAuthConfig;
            this._repoServiceSubscriber = repoServiceSubscriber;
        }

        public void SendData(ITargetData targetData, MessageMetadata metadata)
        {
            var commonData = targetData as CommonTargetData;
            if (commonData == null)
            {
                throw new Exception("StandardScmApiProcessor 无法是识别 targetData，<targetData as CommonTargetData>");
            }

            /*
             * 【方法一】：提供缓存Key
             * cache.Get<string>("cache-key",()=> GetSsid(userName,possword));
             * cache.Get<ScmToken>("cache-key",()=> GetToken(apiKey or Ssid));
             * 
             * 【方法二】：不提供缓存Key
             * cache.Get<string>(TimeSpan.FromMinutes(10),()=> GetSsid(userName,possword));
             * cache.Get<ScmToken>(()=> GetToken(apiKey or Ssid));
             */
            
            // TODO: 需要DDD 重构
            var systemService = _repoSystemService.FirstOrDefault(x => x.ActionCode == metadata.ActionCode);
            var subscriber = _repoServiceSubscriber.FirstOrDefault(x => x.TenantCode == metadata.TenantCode && x.SystemServiceId == systemService.Id);

            if (subscriber == null)
            {
                throw new Exception($"StandardScmApiProcessor: 获取订阅信息失败！不存在订阅信息： TenantCode-<{metadata.TenantCode}>, ActionCode-<{metadata.ActionCode}>");
            }

            //获取ApiKey
            var tenant = _repoTenant.Single(x => x.Code == metadata.TenantCode);
            if (tenant == null)
            {
                throw new Exception($"StandardScmApiProcessor: 获取租户信息失败！tenantCode {metadata.TenantCode}");
            }
            var apiKey = _repoTenantApiKey.Get(subscriber.ApiKeyId);
            if (tenant == null)
            {
                throw new Exception($"StandardScmApiProcessor: 获取apiKey信息失败！ApiKeyId {subscriber.ApiKeyId}");
            }

            //获取 authConfig
            var authConfig = _repoSystemServiceAuthConfig.Get(systemService.AuthConfigId);

            AdditionalAuthInformation(authConfig, apiKey, ref commonData);
            SendRequest(systemService, commonData.JsonString);
        }

        protected void SendRequest(SystemService info, string jsonBody)
        {
            //TODO： 获取并设置认证信息
            var request = new RestRequest(info.Resource, DataFormat.Json);
            request.AddJsonBody(jsonBody);

            var client = new RestClient(info.BaseUrl);
            var response = client.Post(request);
            
        }

        protected virtual void AdditionalAuthInformation(SystemServiceAuthConfig authConfig, TenantApiKey apiKey, ref CommonTargetData data)
        {
            // 此处为和老系统的交互
            var client = new RestClient($"{authConfig.BaseUrl}:{authConfig.Port}");
            var request = new RestRequest(authConfig.Resource, DataFormat.Json);

            request.AddQueryParameter("apikey", apiKey.Value);
            var response = client.Get<dynamic>(request);

            if (response.StatusCode != HttpStatusCode.OK && response.Data["resultCode"] != 0)
            {
                _logger.Error("获取令牌失败！");
                throw new Exception("StandardScmApiProcessor:获取令牌失败！");
            }

            var token = response.Data["token"];

            if (string.IsNullOrEmpty(token))
            {
                _logger.Error("获取令牌失败！");
                throw new Exception("StandardScmApiProcessor:获取令牌失败！");
            }
            
            //设置token
            data.DictionaryData.Add(authConfig.TokenName, token); // add token
        }
    }
}
