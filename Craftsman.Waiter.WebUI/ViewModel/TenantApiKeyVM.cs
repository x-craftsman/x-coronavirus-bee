using Craftsman.Waiter.Domain.SysEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.Waiter.WebUI.ViewModel
{
    public class TenantApiKeyVM
    {
        [Required(ErrorMessage = "未提供Api Key 名称，  name 不能为空！")]
        public string Name { get; set; }
        [Required(ErrorMessage = "未提供Api Key 值，  value 不能为空！")]
        public string Value { get; set; }
    }
}
