using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.Entities
{
    public class BaseEntity : IEntity<int>
    {
        /// <summary>
        /// 获取或设置Topic Id
        /// </summary>
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("updated_by")]
        public int UpdatedBy { get; set; }
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }

        public void SetCommonFileds(ICurrentUser user)
        {
            SetCommonFileds(user, false);
        }
        public void SetCommonFileds(ICurrentUser user, bool isCreated = false)
        {
            if (isCreated)
            {
                this.CreateTime = DateTime.Now;
                this.CreatedBy = user.Id;
            }
            this.UpdateTime = DateTime.Now;
            this.UpdatedBy = user.Id;
        }
        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
}
