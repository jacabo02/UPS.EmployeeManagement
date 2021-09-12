using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace UPS.EmployeeManagement.Model
{
    public class Employee : MessageBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string EMail { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
