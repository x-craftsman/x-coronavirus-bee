using Craftsman.Core.DapperExtensions;

namespace Craftsman.xCoronavirus.Domain.Entities
{
    [Table("contact")]
    public class Contact : BaseEntity
    {
        [Column("hospital_id")]
        public string HospitalId { get; set; }
        [Column("volunteer_name")]
        public string VolunteerName { get; set; }
        [Column("hospital_contact")]
        public string HospitalContact { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
    }
}