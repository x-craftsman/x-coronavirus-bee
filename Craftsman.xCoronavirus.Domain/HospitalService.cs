using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.xCoronavirus.Domain.Entities;
using Craftsman.xCoronavirus.Domain.Repositories;
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
        List<Hospital> GetHospitals(string name);
        Hospital GetHospital(string id);
        List<MaterialDemand> GetHospitalMedicalSupplies(string hospitalId);
        MaterialDemand CreateHospitalMedicalSupply(string hospitalId, MaterialDemand entity);
        MaterialDemand UpdateHospitalMedicalSupply(string hospitalId, string materialDemandId, MaterialDemand entity);
        void DeleteHospitalMedicalSupply(string hospitalId, string materialDemandId);

        List<Contact> GetHospitalContacts(string hospitalId);
        Contact CreateHospitalContact(string hospitalId, Contact entity);
        Contact UpdateHospitalContact(string hospitalId, string contactId, Contact entity);
        void DeleteHospitalContact(string hospitalId, string contactId);
    }
    public class HospitalService : IHospitalService
    {
        private IHospitalRepository _repoHospital;
        public HospitalService(
            IHospitalRepository repoHospital
        )
        {
            _repoHospital = repoHospital;
        }

        public Hospital CreateHospital(Hospital entity)
        {
            //Check

            if (_repoHospital.ExistHospital(x => x.HospitalCode == entity.HospitalCode || x.Name == entity.Name))
            {
                throw new BusinessException($"已经存在同名或者代码（Code）相同的医院！Code = <{entity.HospitalCode}> , Name = <{entity.Name}>");
            }
            //entity.Id = Guid.NewGuid().ToString();

            //foreach (var materialDemand in entity.MaterialDemands)
            //{ 
            //    materialDemand.Id = Guid.NewGuid().ToString();
            //}
            //foreach (var contact in entity.Contacts)
            //{
            //    contact.Id = Guid.NewGuid().ToString();
            //}
            return _repoHospital.CreateHospital(entity);
        }
        public List<Hospital> GetHospitals()
        {
            return _repoHospital.GetAllHospitals();
        }

        public List<Hospital> GetHospitals(string name)
        {
            List<Hospital> hospitals = _repoHospital.QueryHospitals(name); ;
            return hospitals;
        }

        public Hospital GetHospital(string id)
        {
            var entity = _repoHospital.GetHospital(id);
            if (entity == null)
            {
                throw new BusinessException($"不存在对应的医院：id = <{entity.Id}>");
            }
            return entity;
        }
        #region MaterialDemand
        public List<MaterialDemand> GetHospitalMedicalSupplies(string hospitalId)
        {
            var hospital = GetHospital(hospitalId);
            return hospital.MaterialDemands;
        }
        public MaterialDemand CreateHospitalMedicalSupply(string hospitalId, MaterialDemand entity)
        {
            var hospital = GetHospital(hospitalId);
            hospital.MaterialDemands.Add(entity);

            _repoHospital.UpdateHospital(hospital);
            return entity;
        }
        public MaterialDemand UpdateHospitalMedicalSupply(string hospitalId, string materialDemandId, MaterialDemand entity)
        {
            var hospital = GetHospital(hospitalId);
            MaterialDemand materialDemand = null;
            for (var i = 0; i < hospital.MaterialDemands.Count; i++)
            {
                materialDemand = hospital.MaterialDemands[i];
                if (materialDemand.Id == materialDemandId)
                {
                    hospital.MaterialDemands[i] = entity;
                    break;
                }
            }
            _repoHospital.UpdateHospital(hospital);
            return materialDemand;
        }
        public void DeleteHospitalMedicalSupply(string hospitalId, string materialDemandId)
        {
            var hospital = GetHospital(hospitalId);
            var count = hospital.MaterialDemands.RemoveAll(x => x.Id == materialDemandId);
            if (count <= 0)
            {
                throw new BusinessException("删除失败！");
            }
            _repoHospital.UpdateHospital(hospital);
        }
        #endregion MaterialDemand

        #region Contact
        public List<Contact> GetHospitalContacts(string hospitalId)
        {
            var hospital = GetHospital(hospitalId);
            return hospital.Contacts;
        }

        public Contact CreateHospitalContact(string hospitalId, Contact entity)
        {
            var hospital = GetHospital(hospitalId);
            hospital.Contacts.Add(entity);

            _repoHospital.UpdateHospital(hospital);
            return entity;
        }

        public Contact UpdateHospitalContact(string hospitalId, string contactId, Contact entity)
        {
            var hospital = GetHospital(hospitalId);
            Contact contact = null;
            for (var i = 0; i < hospital.Contacts.Count; i++)
            {
                contact = hospital.Contacts[i];
                if (contact.Id == contactId)
                {
                    hospital.Contacts[i] = entity;
                    break;
                }
            }
            _repoHospital.UpdateHospital(hospital);
            return contact;
        }
        public void DeleteHospitalContact(string hospitalId, string contactId)
        {
            var hospital = GetHospital(hospitalId);
            var count = hospital.Contacts.RemoveAll(x => x.Id == contactId);
            if (count <= 0)
            {
                throw new BusinessException("删除失败！");
            }
            _repoHospital.UpdateHospital(hospital);
        }
        #endregion MaterialDemand
    }
}
