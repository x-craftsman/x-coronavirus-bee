using Craftsman.Core.Common;
using Craftsman.Core.CustomizeException;
using Craftsman.Core.DapperExtensions;
using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Craftsman.LaunchPad.Domain.Entities
{
    /// <summary>
    /// Consumer（消费App）信息
    /// </summary>
    public class Consumer : IEntity
    {
        #region  Field
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("key")]
        public string Key { get; set; }
        [Column("secret")]
        public string Secret { get; set; }
        [Column("is_enabled")]
        public bool IsEnabled { get; set; }

        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        [Column("updated_by")]
        public int UpdatedBy { get; set; }
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
        
        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
        #endregion  Field

        #region Action
        /// <summary>
        /// 生成或重置consumer key & secret
        /// </summary>
        /// <returns></returns>
        public void GenerateOrResetKey()
        {
            //set data.
            if (this.Code == null || this.Name == null)
            {
                throw new VerifyException("Consumer对象，code 或 name 为空!");
            }

            var secret = DateTime.Now.Ticks;    //可以添加复杂逻辑。这个不够高级

            this.Key = UtilitiesTool.MD5EncryptString($"{this.Name}-{this.Code}-{secret}");
            this.Secret = secret.ToString();
            this.IsEnabled = true;

            //TODO: 此处暂时写死， 后续提供通用处理逻辑。
            this.CreatedBy = 1;
            this.CreateTime = DateTime.Now;
            this.UpdatedBy = 1;
            this.UpdateTime = DateTime.Now;
        }
        /// <summary>
        /// 启用消费App（Consumer）
        /// </summary>
        public void Enabled()
        {
            this.IsEnabled = true;
        }
        /// <summary>
        /// 禁用消费App（Consumer）
        /// </summary>
        public void Disabled()
        {
            this.IsEnabled = false;
        }
        #endregion Action
    }
}
