using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.CustomizeException
{
    public class VerifyException : Exception
    {
        public string Code { get; set; }
        public VerifyException() : base() { }
        public VerifyException(string message) : base(message) { }
        public VerifyException(string message, string code) : base(message)
        {
            this.Code = code;
        }
        public VerifyException(string message, Exception innerException) : base(message, innerException) { }
        public VerifyException(string message, Exception innerException, string code) : base(message, innerException)
        {
            this.Code = code;
        }
    }
}
