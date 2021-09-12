using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UPS.EmployeeManagement.Model;
using UPS.EmployeeManagement.Presentation.Messages;
using UPS.EmployeeManagement.Service;

namespace UPS.EmployeeManagement.Presentation.ViewModel
{
    public class EmployeeViewModel : ViewModelBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeeViewModel(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        private string name;

        public bool ForUpdate { get; set; }

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string email;

        public string Email
        {
            get => email;
            set => Set(ref email, value);
        }

        private string gender = "all";

        public string Gender
        {
            get => gender;
            set => Set(ref gender, value);
        }


        private string status = "all";

        public string Status
        {
            get => status;
            set => Set(ref status, value);
        }

        private List<string> statuses = new List<string>
        {
            "all", "active", "inactive"
        };

        public List<string> Statuses => statuses;

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ?? (saveCommand = new RelayCommand(Add));

                async void Add()
                {
                    CreateEmployeeResponse result = null;
                    if (ForUpdate)
                    {
                        result = await employeeService.UpdateEmployee(new Model.Employee
                        {
                            Id = Convert.ToInt32(Id),
                            EMail = Email,
                            Name = Name,
                            Gender = Gender,
                            Status = Status
                        });
                    }
                    else
                    {
                        result = await employeeService.CreateEmployee(new Model.Employee
                        {
                            EMail = Email,
                            Name = Name,
                            Gender = Gender,
                            Status = Status
                        });
                    }

                    MessengerInstance.Send(new CloseDialogMessage
                    {
                        IsSuccess = result != null
                    });
                }
            }
        }
    }
}
