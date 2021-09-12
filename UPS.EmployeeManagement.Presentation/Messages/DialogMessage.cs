using System;
using System.Collections.Generic;
using System.Text;

namespace UPS.EmployeeManagement.Presentation.Messages
{
    public class DialogMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }

        public bool IsError { get; set; }
    }
}
