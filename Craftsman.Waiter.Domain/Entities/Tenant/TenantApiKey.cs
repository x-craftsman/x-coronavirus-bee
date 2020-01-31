using Craftsman.Core.DapperExtensions;
using Craftsman.Waiter.Domain.SysEnum;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("tenant_apikey")]
    public class TenantApiKey : BaseEntity
    {
        [Column("tenant_id")]
        public int TenantId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("value")]
        public string Value { get; set; }
        [Column("status")]
        public AvailableState Status { get; set; }
        [NotMapped]
        public Tenant Tenant { get; set; }
    }
}

