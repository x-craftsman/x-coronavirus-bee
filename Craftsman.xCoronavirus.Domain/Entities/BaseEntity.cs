using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.xCoronavirus.Domain.Entities
{
    public class BaseEntity : IEntity<string>
    {
        /// <summary>
        /// 获取或设置 Id
        /// </summary>
        [Key, Required, Column("id")]
        public string Id { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
}
