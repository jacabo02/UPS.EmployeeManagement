using System;
using System.Collections.Generic;
using System.Linq;

namespace UPS.EmployeeManagement.Model
{
    public class SearchParams
    {
        public SearchParams()
        {
            KeyValuePairs = new Dictionary<string, string>();
        }
        public Dictionary<string, string> KeyValuePairs { get; set; }

        public string QueryString
        {
            get
            {
                return string.Join("&", KeyValuePairs
                    .Select(t => string.Format("{0}={1}", t.Key, t.Value)));
            }
        }
    }

}