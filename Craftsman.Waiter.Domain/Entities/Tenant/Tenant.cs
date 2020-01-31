using Craftsman.Core.DapperExtensions;
using Craftsman.Waiter.Domain.SysEnum;
using System.Collections.Generic;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("tenant")]
    public class Tenant : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("nickname")]
        public string Nickname { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("status")]
        public AvailableState Status { get; set; }
        [Column("contact_name")]
        public string ContactName { get; set; }
        [Column("contact_tel")]
        public string ContactTel { get; set; }
        [Column("contact_email")]
        public string ContactEmail { get; set; }

        [NotMapped]
        public List<TenantApiKey> ApiKeys { get; set; }
    }
}
