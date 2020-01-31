using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;

namespace Craftsman.xCoronavirus.Domain.Entities
{
    [Table("hospital")]
    public class Hospital : IEntity<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("city")]
        public string City { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("staff_count")]
        public string StaffCount { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("is_verify_qualification")]
        public string IsVerifyQualification { get; set; }
        [Column("verify_content")]
        public string VerifyContent { get; set; }
        [Column("can_receive")]
        public string CanReceive { get; set; }
        [Column("issue")]
        public string Issue { get; set; }
        [Column("level")]
        public string Level { get; set; }

        public bool IsTransient()
        {
            throw new System.NotImplementedException();
        }
    }
}

