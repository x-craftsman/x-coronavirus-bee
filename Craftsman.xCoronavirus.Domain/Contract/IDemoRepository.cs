using Craftsman.Core.Domain.Repositories;
using Craftsman.xCoronavirus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Craftsman.xCoronavirus.Domain.Repositories
{
    public interface IHospitalRepository : IRepository
    {
        Hospital GetHospital(string guid);
        List<Hospital> GetAllHospitals(bool withDetails = false);
        List<Hospital> QueryHospitals(string name);

        Hospital CreateHospital(Hospital entity);
        Hospital UpdateHospital(Hospital entity);
        void DeleteHospital(Hospital entity);
        bool ExistHospital(Expression<Func<Hospital, bool>> predicate);
    }
}
