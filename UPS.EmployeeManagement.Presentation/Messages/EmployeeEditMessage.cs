using System;
using System.Collections.Generic;
using System.Text;
using UPS.EmployeeManagement.Presentation.ViewModel;

namespace UPS.EmployeeManagement.Presentation.Messages
{
    public class EmployeeEditMessage
    {
        public EmployeeViewModel Employee { get; set; }

        public bool? DialogResult { get; set; }
    }
}
