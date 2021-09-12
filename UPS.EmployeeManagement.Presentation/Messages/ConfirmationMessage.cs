using System;
using System.Collections.Generic;
using System.Text;

namespace UPS.EmployeeManagement.Presentation.Messages
{
    public class ConfirmationMessage
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public bool? Result { get; set; }
    }
}
