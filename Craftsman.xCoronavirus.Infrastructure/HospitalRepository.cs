using Craftsman.Core.Domain.Repositories;
using Craftsman.xCoronavirus.Domain.Entities;
using Craftsman.xCoronavirus.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Craftsman.xCoronavirus.Infrastructure
{
    public class HospitalRepository : IHospitalRepository
    {
        private IRepository<Hospital, string> _repoHospital;
        private IRepository<MaterialDemand, string> _repoMaterialDemand;
        private IRepository<Contact, string> _repoContact;

        public HospitalRepository(
            IRepository<Hospital, string> repoHospital,
            IRepository<MaterialDemand, string> repoMaterialDemand,
            IRepository<Contact, string> repoContact
        )
        {
            _repoHospital = repoHospital;
            _repoMaterialDemand = repoMaterialDemand;
            _repoContact = repoContact;
        }

        public bool ExistHospital(Expression<Func<Hospital, bool>> predicate)
        {
            return !(_repoHospital.FirstOrDefault(predicate) == null);
        }
        public Hospital GetHospital(string guid)
        {
            var hospital = _repoHospital.Get(guid);
            if (hospital == null) return null;

            hospital.MaterialDemands = _repoMaterialDemand.GetAllList(x => x.HospitalId == hospital.Id);
            hospital.Contacts = _repoContact.GetAllList(x => x.HospitalId == hospital.Id);

            return hospital;
        }

        public List<Hospital> GetAllHospitals(bool withDetails = false)
        {
            var hospitals = _repoHospital.GetAllList();
            if (hospitals == null) return null;

            if (withDetails)
            {
                foreach (var hospital in hospitals)
                {
                    hospital.MaterialDemands = _repoMaterialDemand.GetAllList(x => x.HospitalId == hospital.Id);
                    hospital.Contacts = _repoContact.GetAllList(x => x.HospitalId == hospital.Id);
                }
            }
            
            return hospitals;
        }

        public List<Hospital> QueryHospitals(string name)
        {
            var hospitals = _repoHospital.GetAllList(x => x.Name.Contains(name));
            if (hospitals == null) return null;

            foreach (var hospital in hospitals)
            {
                hospital.MaterialDemands = _repoMaterialDemand.GetAllList(x => x.HospitalId == hospital.Id);
                hospital.Contacts = _repoContact.GetAllList(x => x.HospitalId == hospital.Id);
            }
            return hospitals;
        }

        public Hospital CreateHospital(Hospital entity)
        {
            _repoHospital.Insert(entity);
            if (entity.MaterialDemands != null && entity.MaterialDemands.Count > 0)
            {
                foreach (var materialDemand in entity.MaterialDemands)
                {
                    _repoMaterialDemand.Insert(materialDemand);
                }
                foreach (var contact in entity.Contacts)
                {
                    _repoContact.Insert(contact);
                }
            }

            return entity;
        }
        public Hospital UpdateHospital(Hospital entity)
        {
            _repoHospital.Update(entity);
            if (entity.MaterialDemands != null && entity.MaterialDemands.Count > 0)
            {
                foreach (var materialDemand in entity.MaterialDemands)
                {
                    _repoMaterialDemand.InsertOrUpdate(materialDemand);
                }
                foreach (var contact in entity.Contacts)
                {
                    _repoContact.InsertOrUpdate(contact);
                }
            }

            return entity;
        }
        public void DeleteHospital(Hospital entity)
        {
            if (entity.MaterialDemands != null && entity.MaterialDemands.Count > 0)
            {
                foreach (var materialDemand in entity.MaterialDemands)
                {
                    _repoMaterialDemand.Delete(materialDemand.Id);
                }
                foreach (var contact in entity.Contacts)
                {
                    _repoContact.Delete(contact.Id);
                }
            }
        }
    }
}
