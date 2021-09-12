using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UPS.EmployeeManagement.Model
{
    public class ResponseMetaModel
    {
        [JsonProperty("pagination")]
        public PaginationModel PaginationModel { get; set; }
    }
}
