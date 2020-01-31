using Craftsman.Core.DapperExtensions;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("service_subscriber_custom_logic")]
    public class ServiceSubscriberCustomLogic : BaseEntity
    {
        [Column("subscriber_id")]
        public int SubscriberId { get; set; }
        [Column("custom_file_id")]
        public int CustomFileId { get; set; }
        [Column("tenant_code")]
        public string TenantCode { get; set; }

        [NotMapped]
        public CustomFile CustomFile { get; set; }
    }
}

