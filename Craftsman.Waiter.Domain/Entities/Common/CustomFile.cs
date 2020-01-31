using Craftsman.Core.DapperExtensions;
using System;

namespace Craftsman.Waiter.Domain.Entities
{
    [Table("custom_file")]
    public class CustomFile : BaseEntity
    {
        [Column("directory_path")]
        public string DirectoryPath { get; set; }
        [Column("original_name")]
        public string OriginalName { get; set; }
        [Column("physical_name")]
        public string PhysicalName { get; set; }
        [Column("size")]
        public int Size { get; set; }
        [Column("type")]
        public int Type { get; set; }
        [Column("tenant_code")]
        public string TenantCode { get; set; }

        /// <summary>
        /// 上传验证
        /// </summary>
        public void Upload()
        {
            // TODO: Validation
            if (string.IsNullOrEmpty(this.OriginalName)|| !this.OriginalName.EndsWith(".dll"))
            {
                throw new Exception("[CustomFile.Upload]:OriginalName不能为空且必须以.dll结尾！");
            }
            
            // generate the physical name.
            var random = new Random();
            this.PhysicalName = $"{this.OriginalName}-{DateTime.Now.ToString("yyyyMMdd-HHmmss")}-{random.Next(1000, 9999)}.dll";
        }
    }
}

