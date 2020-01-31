using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Craftsman.Core.CustomizeException
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorObject = new ErrorObject();
            var status = 500;
            if (ex is BusinessException)
            {
                var bizException = ex as BusinessException;
                errorObject.Errors.Add(new ErrorItem
                {
                    Status = (int)bizException.Status,
                    Code = bizException.Code,
                    Message = bizException.Message
                });
                status = (int)bizException.Status;
            }
            else
            {
                errorObject.Errors.Add(new ErrorItem
                {
                    Status = 500,
                    Code = 9999,
                    Message = ex.Message
                });
            }

            //LoggerManager logger = new LoggerManager();
            //logger.Error(ex, ex.Message);

            //context.Response.StatusCode
            var result = JsonConvert.SerializeObject(errorObject);
            context.Response.ContentType = "application/json;charset=utf-8";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

