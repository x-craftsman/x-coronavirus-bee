using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Craftsman.Core.CustomizeException
{
    public class BusinessException : Exception
    {

        public HttpStatusCode Status { get; set; }
        public int Code { get; set; }

        public BusinessException() : base() { }
        public BusinessException(string message) : this(message: message, status: HttpStatusCode.BadRequest, code: 9999) { }
        public BusinessException(string message, int code) : this(message: message, status: HttpStatusCode.BadRequest, code: code) { }

        public BusinessException(string message, HttpStatusCode status) : this(message: message, status: status, code: 9999) { }

        public BusinessException(string message, HttpStatusCode status, int code) : this(message, null, code, status) { }


        public BusinessException(string message, Exception innerException) : this(message, innerException, 9999, HttpStatusCode.BadRequest) { }

        public BusinessException(string message, Exception innerException, int code) : this(message, innerException, code, HttpStatusCode.BadRequest) { }

         
        public BusinessException(string message, Exception innerException, int code, HttpStatusCode status) : base(message, innerException)
        {
            this.Code = code;
            this.Status = status;
        }

    }
}
