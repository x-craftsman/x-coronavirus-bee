using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;

namespace Craftsman.xCoronavirus.Domain.Entities
{
    [Table("require_detail")]
    public class MedicalSupply : IEntity<int>
    {
        [NotMapped]
        public int Id { get; set; }
        [Column("hospital_id")]
        public int HospitalId { get; set; }
        [Column("material_name")]
        public string Name { get; set; }
        [Column("material_count")]
        public string Count { get; set; }
        [Column("level")]
        public string Level { get; set; }
        [Column("issue")]
        public string Issue { get; set; }

        public bool IsTransient()
        {
            throw new System.NotImplementedException();
        }
    }
}

