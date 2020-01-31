using Craftsman.Core.Domain.Repositories;
using Craftsman.Waiter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Craftsman.Waiter.Domain.RepositoryContract
{
    public interface IServiceSubscriberRepository : IRepository
    {
        IQueryable<ServiceSubscriber> GetAll();
        ServiceSubscriber GetServiceSubscriber(string actionCode, string tenantCode);
        void DeleteServiceSubscriber(int id);
        ServiceSubscriber GetServiceSubscriber(int id);
        ServiceSubscriber Insert(ServiceSubscriber entity);
    }
}
