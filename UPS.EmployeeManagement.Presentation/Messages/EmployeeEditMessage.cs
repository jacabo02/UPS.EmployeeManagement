using System;
using System.Collections.Generic;
using System.Text;
using UPS.EmployeeManagement.Presentation.ViewModel;

namespace UPS.EmployeeManagement.Presentation.Messages
{
    public class EmployeeEditMessage
    {
        public bool ForUpdate { get; set; }

        public EmployeeViewModel Employee { get; set; }

        public bool? DialogResult { get; set; }
    }
}
