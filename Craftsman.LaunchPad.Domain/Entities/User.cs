using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.Entities
{
    /// <summary>
    /// 用户（User）信息
    /// </summary>
    public class User : IEntity, ICurrentUser
    {
        #region Field
        [Key, Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public int Type { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("updated_by")]
        public int UpdatedBy { get; set; }
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
        #endregion Field

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
}
