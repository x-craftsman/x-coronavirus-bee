using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.xCoronavirus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Craftsman.xCoronavirus.Domain
{
    public interface IHospitalService : IService
    {
        Hospital CreateHospital(Hospital entity);
        List<Hospital> GetHospitals();
        List<Hospital> GetHospitals(string city, string name);
        Hospital GetHospital(int id);

        List<MedicalSupply> GetHospitalMedicalSupplies(int hospitalId);
    }
    public class HospitalService: IHospitalService
    {
        private IRepository<Hospital> _repoHospital;
        private IRepository<MedicalSupply> _repoMedicalSupply;
        public HospitalService(
            IRepository<Hospital> repoHospital,
            IRepository<MedicalSupply> repoMedicalSupply
        )
        {
            _repoHospital = repoHospital;
            _repoMedicalSupply = repoMedicalSupply;
        }

        public Hospital CreateHospital(Hospital entity)
        {
            //Check
            if (_repoHospital.FirstOrDefault(x => x.Id == entity.Id || x.Name == entity.Name) != null)
            {
                throw new BusinessException($"已经存在同名或者代码（Id）相同的医院！Code = <{entity.Id}> , Name = <{entity.Name}>");
            }
            return _repoHospital.Insert(entity);
        }
        public List<Hospital> GetHospitals()
        {
            return _repoHospital.GetAllList();
        }

        public List<Hospital> GetHospitals(string city, string name)
        {
            List<Hospital> list;
            if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(name))
            {
                list = _repoHospital.GetAllList();
            }
            else if (!string.IsNullOrEmpty(city) && string.IsNullOrEmpty(name))
            {
                list = _repoHospital.GetAllList(x => x.City.Contains(city));
            }
            else if (string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(name))
            {
                list = _repoHospital.GetAllList(x => x.Name.Contains(name));
            }
            else
            {
                list = _repoHospital.GetAllList(x => x.City.Contains(city) && x.Name.Contains(name)).ToList();
            }
            return list;
        }

        public Hospital GetHospital(int id)
        {
            var entity = _repoHospital.Get(id);
            if (entity == null)
            {
                throw new BusinessException($"不存在对应的医院：id = <{entity.Id}>");
            }
            return entity;
        }

        public List<MedicalSupply> GetHospitalMedicalSupplies(int hospitalId)
        {
            return _repoMedicalSupply.GetAllList(x =>x.HospitalId == hospitalId);
        }
    }
}
