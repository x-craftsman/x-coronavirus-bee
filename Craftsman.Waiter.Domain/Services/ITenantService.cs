using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Runtime;
using Craftsman.Waiter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.Services
{
    public interface ITenantService : IService
    {
        Tenant CreateTenant(Tenant entity);
        Tenant UpdateTenant(Tenant entity);
        void DeleteTenant(int id);

        List<Tenant> GetTenants();
        Tenant GetTenant(int id);

        TenantApiKey CreateTenantApiKey(TenantApiKey entity);
        TenantApiKey UpdateTenantApiKey(TenantApiKey entity);
        void DeleteTenantApiKey(int tenantId, int apiKeyId);

        List<TenantApiKey> GetTenantApiKeys(int tenantId);
        TenantApiKey GetTenantApiKey(int tenantId, int apiKeyId);
    }

    public class TenantService : ITenantService
    {
        public ISession Session { get; set; }
        public IRepository<Tenant> repoTenant { get; set; }
        public IRepository<TenantApiKey> repoTenantApiKey { get; set; }

        #region Tenant
        public Tenant CreateTenant(Tenant entity)
        {
            //Check
            if (repoTenant.FirstOrDefault(x => x.Code == entity.Code || x.Name == entity.Name) != null)
            {
                throw new BusinessException($"已经存在同名或者代码（Code）相同的租户！Code = <{entity.Code}> , Name = <{entity.Name}>");
            }

            entity.SetCommonFileds(Session.CurrentUser, true);
            return repoTenant.Insert(entity);
        }

        public Tenant UpdateTenant(Tenant entity)
        {
            //Check
            var tempEntity = repoTenant.Get(entity.Id);
            if (tempEntity == null)
            {
                throw new BusinessException($"不存在经存在对应租户：id = <{entity.Id}>");
            }

            //Action
            entity.SetCommonFileds(Session.CurrentUser);
            return repoTenant.Update(entity);
        }
        public void DeleteTenant(int id)
        {
            //Check
            var tempEntity = repoTenant.Get(id);
            if (tempEntity == null)
            {
                throw new BusinessException($"不存在经存在对应的租户：id = <{id}>");
            }

            //Action
            repoTenant.Delete(id);
        }
        public List<Tenant> GetTenants()
        {
            return repoTenant.GetAllList();
        }

        public Tenant GetTenant(int id)
        {
            var entity = repoTenant.Get(id);
            if (entity == null)
            {
                throw new BusinessException($"不存在对应的租户：id = <{entity.Id}>");
            }
            return entity;
        }
        #endregion Tenant

        #region Tenant ApiKey
        public TenantApiKey CreateTenantApiKey(TenantApiKey entity)
        {
            //Check
            if (repoTenantApiKey.FirstOrDefault(x => x.Name == entity.Name || x.Value == entity.Value) != null)
            {
                throw new BusinessException($"已经存在同名或者Key值相同的数据！Name = <{entity.Name}> , Value = <{entity.Value}>");
            }

            entity.SetCommonFileds(Session.CurrentUser, true);
            return repoTenantApiKey.Insert(entity);
        }

        public TenantApiKey UpdateTenantApiKey(TenantApiKey entity)
        {
            //Check
            var tempEntity = repoTenantApiKey.Get(entity.Id);
            if (tempEntity == null)
            {
                throw new BusinessException($"不存在经存在对应Api Key：id = <{entity.Id}>");
            }

            //Action
            entity.SetCommonFileds(Session.CurrentUser);
            return repoTenantApiKey.Update(entity);
        }
        public void DeleteTenantApiKey(int tenantId, int apiKeyId)
        {
            //Check
            var tenant = repoTenant.Get(tenantId);
            if (tenant == null)
            {
                throw new BusinessException($"不存在对应的租户：id = <{tenant.Id}>");
            }

            var entity = repoTenantApiKey.Single(x => x.TenantId == tenantId && x.Id == apiKeyId);
            if (entity == null)
            {
                throw new BusinessException($"不存在对应的ApiKey：apiKeyId = <{entity.Id}>, TenantId = <{entity.TenantId}>");
            }

            //Action
            repoTenantApiKey.Delete(apiKeyId);
        }
        public List<TenantApiKey> GetTenantApiKeys(int tenantId)
        {
            return repoTenantApiKey.GetAllList(x => x.TenantId == tenantId);
        }

        public TenantApiKey GetTenantApiKey(int tenantId, int apiKeyId)
        {
            var tenant = repoTenant.Get(tenantId);
            if (tenant == null)
            {
                throw new BusinessException($"不存在对应的租户：id = <{tenant.Id}>");
            }

            var entity = repoTenantApiKey.Single(x => x.TenantId == tenantId && x.Id == apiKeyId);
            if (entity == null)
            {
                throw new BusinessException($"不存在对应的ApiKey：apiKeyId = <{entity.Id}>, TenantId = <{entity.TenantId}>");
            }
            return entity;
        }
        #endregion Tenant ApiKey
    }
}
