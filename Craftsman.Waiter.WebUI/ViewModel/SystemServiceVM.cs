using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.Waiter.WebUI.ViewModel
{
    public class SystemServiceVM
    {
        [Required(ErrorMessage = "未提供操作代码，  authConfigId 不能为空！")]
        public int? AuthConfigId { get; set; }
        [Required(ErrorMessage = "未提供操作代码，actionCode 不能为空！")]
        public string ActionCode { get; set; }
        [Required(ErrorMessage = "未提供系统代码，systemCode 不能为空！")]
        public string SystemCode { get; set; }
        [Required(ErrorMessage = "未提供服务地址，baseUrl 不能为空！")]
        public string BaseUrl { get; set; }
        [Required(ErrorMessage = "未提供资源地址，resource 不能为空！")]
        public string Resource { get; set; }
        
        public int Port { get; set; }
        public int DataFormat { get; set; }
    }
}
