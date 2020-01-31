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

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    public class TextScmApiProcessor : IProcessor
    {
        private ILogger _logger;
        private IRepository<SystemService> _repoSystemService;
        private IRepository<SystemServiceAuthConfig> _repoSystemServiceAuthConfig;
        private IRepository<Tenant> _repoTenant;
        private IRepository<TenantApiKey> _repoTenantApiKey;
        //private ICache cache;
        public TextScmApiProcessor(
            ILogger logger,
            IRepository<SystemService> repoSystemService,
            IRepository<SystemServiceAuthConfig> repoSystemServiceAuthConfig,
            IRepository<Tenant> repoTenant,
            IRepository<TenantApiKey> repoTenantApiKey
        )
        {
            this._logger = logger;
            this._repoSystemService = repoSystemService;
            this._repoSystemServiceAuthConfig = repoSystemServiceAuthConfig;
            this._repoTenant = repoTenant;
            this._repoTenantApiKey = repoTenantApiKey;
        }

        public void SendData(ITargetData targetData, MessageMetadata metadata)
        {
            var commonData = targetData as CommonTargetData;
            if (commonData == null)
            {
                throw new Exception("TextScmApiProcessor 无法是识别 targetData，<targetData as CommonTargetData>");
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

            var systemService = _repoSystemService.FirstOrDefault(x => x.ActionCode == metadata.ActionCode);

            //获取ApiKey
            var tenant = _repoTenant.Single(x => x.Code == metadata.TenantCode);
            if (tenant == null)
            {
                throw new Exception($"TextScmApiProcessor: 获取租户信息失败！tenantCode {metadata.TenantCode}");
            }
            var apiKey = _repoTenantApiKey.Single(x => x.TenantId == tenant.Id);

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
            var client = new RestClient($"{authConfig.BaseUrl}:{authConfig.Port}");
            var request = new RestRequest(authConfig.Resource, DataFormat.Json);

            request.AddQueryParameter("apikey", apiKey.Value);
            var response = client.Get<dynamic>(request);

            if (response.StatusCode != HttpStatusCode.OK && response.Data["resultCode"] != 0)
            {
                _logger.Error("获取令牌失败！");
                throw new Exception("SimpleApiProcessor:获取令牌失败！");
            }

            var token = response.Data["token"];

            if (string.IsNullOrEmpty(token))
            {
                _logger.Error("获取令牌失败！");
                throw new Exception("SimpleApiProcessor:获取令牌失败！");
            }
            
            //设置token
            data.DictionaryData.Add(authConfig.TokenName, token);
        }
    }
}
