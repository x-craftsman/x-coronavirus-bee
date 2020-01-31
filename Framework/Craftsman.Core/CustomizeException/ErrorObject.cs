using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.CustomizeException
{
    public class ErrorObject
    {
        public ErrorObject()
        {
            this.Errors = new List<ErrorItem>();
        }
        [JsonProperty("errors")]
        public List<ErrorItem> Errors { get; set; }

    }

    public class ErrorItem
    {
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }


    
}
