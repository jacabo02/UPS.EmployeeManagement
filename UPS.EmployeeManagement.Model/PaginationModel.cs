using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace UPS.EmployeeManagement.Model
{
    public class PaginationModel
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }
    }
}
