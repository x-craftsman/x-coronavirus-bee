using Craftsman.Core.DapperExtensions;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("system_service_auth_config")]
    public class SystemServiceAuthConfig : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("base_url")]
        public string BaseUrl { get; set; }
        [Column("resource")]
        public string Resource { get; set; }
        [Column("port")]
        public int Port { get; set; }
        [Column("token_name")]
        public string TokenName { get; set; }
    }
}

