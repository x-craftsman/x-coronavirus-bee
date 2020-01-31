using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;

namespace Craftsman.xCoronavirus.Domain.Entities
{
    [Table("cantact")]
    public class Cantact : IEntity<int>
    {
        [NotMapped]
        public int Id { get; set; }
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("person_in_charge")]
        public string PersonInCharge { get; set; }
        [Column("hospital_contact")]
        public string Contact { get; set; }
        [Column("hospital_contact_phone")]
        public string ContactPhone { get; set; }
        [Column("issue")]
        public string Issue { get; set; }
        
        public bool IsTransient()
        {
            throw new System.NotImplementedException();
        }
    }
}

