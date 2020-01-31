using Craftsman.Core.DapperExtensions;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("system_service")]
    public class SystemService : BaseEntity
    {
        [Column("auth_config_id")]
        public int AuthConfigId { get; set; }
        [NotMapped]
        public SystemServiceAuthConfig AuthConfig { get; set; }
        [Column("action_code")]
        public string ActionCode { get; set; }
        [Column("system_code")]
        public string SystemCode { get; set; }
        [Column("base_url")]
        public string BaseUrl { get; set; }
        [Column("resource")]
        public string Resource { get; set; }
        [Column("port")]
        public int Port { get; set; }
        [Column("status")]
        public int Status { get; set; }
    }
}

