using Craftsman.Core.DapperExtensions;

namespace Craftsman.xCoronavirus.Domain.Entities
{
    [Table("material_demand")]
    public class MaterialDemand : BaseEntity
    {
        [Column("hospital_id")]
        public string HospitalId { get; set; }
        [Column("demand_name")]
        public string DemandName { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("unit")]
        public string Unit { get; set; }
        [Column("level")]
        public int Level { get; set; }
        [Column("remark")]
        public string Remark { get; set; }
    }
}
