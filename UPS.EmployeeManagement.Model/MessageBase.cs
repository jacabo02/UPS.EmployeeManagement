using Newtonsoft.Json;

namespace UPS.EmployeeManagement.Model
{
    public class MessageBase
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
