using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace UPS.EmployeeManagement.Model
{
    public class GetEmployeeResponseModel : ResponseBase
    {

        [JsonProperty("data")]
        public List<Employee> EmployeeList { get; set; }
    }
}
