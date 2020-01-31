using Craftsman.Waiter.Domain.SysEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.Waiter.WebUI.ViewModel
{
    public class TenantVM
    {
        [Required(ErrorMessage = "未提供租户名称，  name 不能为空！")]
        public string Name { get; set; }
        [Required(ErrorMessage = "未提供租户代码，  code 不能为空！")]
        public string Code { get; set; }
        [Required(ErrorMessage = "未提供租户别名，  nickname 不能为空！")]
        public string Nickname { get; set; }
        [Required(ErrorMessage = "未提供租户类型，  type 不能为空！")]
        public int Type { get; set; }
        [Required(ErrorMessage = "未提供租户状态，  status 不能为空！")]
        public AvailableState Status { get; set; }
        [Required(ErrorMessage = "未提供联系人名称，  contact_name 不能为空！")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "未提供联系人电话，  contact_tel 不能为空！")]
        public string ContactTel { get; set; }
        [Required(ErrorMessage = "未提供联系人邮箱，  contact_email 不能为空！")]
        public string ContactEmail { get; set; }
    }
}
