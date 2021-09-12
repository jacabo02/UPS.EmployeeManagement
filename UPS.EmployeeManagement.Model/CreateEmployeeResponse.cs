using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UPS.EmployeeManagement.Model
{
    public class CreateEmployeeResponse : ResponseBase
    {

        [JsonProperty("data")]
        public Employee Employee { get; set; }
    }
}
