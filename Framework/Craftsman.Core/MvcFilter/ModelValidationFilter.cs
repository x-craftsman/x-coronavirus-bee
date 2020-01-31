using Craftsman.Core.CustomizeException;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.MvcFilter
{
    public class ModelValidationFilter : IActionFilter
    {
        /// <summary>
        /// 操作执行结束之后，模型绑定操作
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context) { }

        /// <summary>
        /// 操作执行之前，验证模型
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //验证模型标签
            if (!context.ModelState.IsValid)
            {
                foreach (var item in context.ModelState.Values)
                {
                    foreach (var error in item.Errors)
                    {
                        throw new BusinessException(error.ErrorMessage, error.Exception);
                    }
                }
            }
        }
    }
}
