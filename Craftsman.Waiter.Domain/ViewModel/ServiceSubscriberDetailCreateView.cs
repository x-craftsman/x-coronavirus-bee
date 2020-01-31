using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Craftsman.Waiter.Domain.ViewModel
{
    public class ServiceSubscriberDetailCreateView
    {
        [Required(ErrorMessage = "Source 字段不能为空！")]
        public string Source { get; set; }
        [Required(ErrorMessage = "Target 字段不能为空！")]
        public string Target { get; set; }
    }
}
