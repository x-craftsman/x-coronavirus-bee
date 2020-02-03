using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.xCoronavirus.Domain.Entities
{
    /// <summary>
    /// 聚合
    /// </summary>
    [Table("hospital")]
    public class Hospital : BaseEntity
    {
        public Hospital()
        {
            this.Contacts = new List<Contact>();
            this.MaterialDemands = new List<MaterialDemand>();
        }
        #region Field
        [Column("hospital_code")]
        public string HospitalCode { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("staff_count")]
        public int StaffCount { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("qualified")]
        public int Qualified { get; set; }
        [Column("qualification_comment")]
        public string QualificationComment { get; set; }
        [Column("direct_accept")]
        public int DirectAccept { get; set; }
        [Column("direct_accept_comment")]
        public string DirectAcceptComment { get; set; }
        [Column("remark")]
        public string Remark { get; set; }

        [NotMapped]
        public List<MaterialDemand> MaterialDemands { get; set; }
        [NotMapped]
        public List<Contact> Contacts { get; set; }
        #endregion Field

        #region Action        
        #endregion Action
    }
}
