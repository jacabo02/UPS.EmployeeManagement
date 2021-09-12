using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace UPS.EmployeeManagement.Model
{
    public class ResponseBase
    {
        [JsonProperty("code")]
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpStatusCode Code { get; set; }

        [JsonProperty("meta")]
        public ResponseMetaModel MetaModel { get; set; }

        public bool IsSuccess => Code == HttpStatusCode.OK;
    }
}
