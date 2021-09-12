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
                    .Where(t => !string.IsNullOrEmpty(t.Value))
                    .Select(t => string.Format("{0}={1}", t.Key, t.Value)));
            }
        }

        public IEnumerable<string> QueryParams
        {
            get
            {
                return KeyValuePairs.Count > 0 ?
                    KeyValuePairs
                    .Where(t => !string.IsNullOrEmpty(t.Value))
                    .Select(t => string.Format("{0}={1}", t.Key, t.Value)) : new List<string>();
            }
        }
    }

}