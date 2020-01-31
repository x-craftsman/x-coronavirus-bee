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
    public interface ISystemServiceService : IService
    {
        SystemService CreateSystemService(SystemService entity);
        SystemService UpdateSystemService(SystemService entity);
        void DeleteSystemService(int id);

        List<SystemService> GetSystemServices();
        SystemService GetSystemService(int id);

        List<SystemServiceAuthConfig> GetSystemServiceAuthConfigs();
    }

    public class SystemServiceService : ISystemServiceService
    {
        public ISession Session { get; set; }
        private IRepository<SystemService> _repoSystemService;
        private IRepository<SystemServiceAuthConfig> _repoSystemServiceAuthConfig;


        public SystemServiceService(
            IRepository<SystemService> repoSystemService,
            IRepository<SystemServiceAuthConfig> repoSystemServiceAuthConfig
        )
        {
            this._repoSystemService = repoSystemService;
            this._repoSystemServiceAuthConfig = repoSystemServiceAuthConfig;
        }

        public SystemService CreateSystemService(SystemService entity)
        {
            //Check
            if (_repoSystemService.FirstOrDefault(x => x.SystemCode == entity.SystemCode && x.ActionCode == entity.ActionCode) != null)
            {
                throw new BusinessException($"已经存在对应WebApi Service：SystemCode = <{entity.SystemCode}> , ActionCode = <{entity.ActionCode}>");
            }

            entity.SetCommonFileds(Session.CurrentUser, true);
            return _repoSystemService.Insert(entity);
        }

        public SystemService UpdateSystemService(SystemService entity)
        {
            //Check
            var tempEntity = _repoSystemService.Get(entity.Id);
            if (tempEntity == null)
            {
                throw new BusinessException($"不存在经存在对应WebApi Service：id = <{entity.Id}>");
            }

            //Action
            entity.SetCommonFileds(Session.CurrentUser);
            return _repoSystemService.Update(entity);
        }
        public void DeleteSystemService(int id)
        {
            //Check
            var tempEntity = _repoSystemService.Get(id);
            if (tempEntity == null)
            {
                throw new BusinessException($"不存在经存在对应WebApi Service：id = <{id}>");
            }

            //Action
            _repoSystemService.Delete(id);
        }
        public List<SystemService> GetSystemServices()
        {
            // TODO: 此处应该有缓存
            return _repoSystemService.GetAllList();
        }

        public SystemService GetSystemService(int id)
        {
            var entity = _repoSystemService.Get(id);
            if (entity == null)
            {
                throw new BusinessException($"不存在对应的系统服务：id = <{entity.Id}>");
            }

            entity.AuthConfig = _repoSystemServiceAuthConfig.Single(x => x.Id == entity.AuthConfigId);
            return entity;
        }
        public List<SystemServiceAuthConfig> GetSystemServiceAuthConfigs()
        {
            return _repoSystemServiceAuthConfig.GetAllList();
        }
    }
}
