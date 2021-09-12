using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UPS.EmployeeManagement.Model;
using UPS.EmployeeManagement.Presentation.Messages;
using UPS.EmployeeManagement.Service;

namespace UPS.EmployeeManagement.Presentation.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IEmployeeService employeeService;

        public MainViewModel(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
            SearchModel = new EmployeeViewModel(employeeService);
        }

        public EmployeeViewModel SearchModel { get; private set; }

        public string EmailLabel => "E-Mail";

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => Set(ref isBusy, value);
        }

        private ICommand searchCommand;

        public ICommand SearchCommand
        {
            get
            {
                return searchCommand ?? (searchCommand = new RelayCommand(Search));

                async void Search()
                {
                    SetCurrentPage(1);
                    await QueryEmployees();
                }
            }
        }

        private async Task QueryEmployees()
        {
            try
            {
                IsBusy = true;
                SearchParams searchParams = new SearchParams();
                if (currentPage > 0)
                {
                    searchParams.KeyValuePairs.Add("page", currentPage.ToString());
                }
                if (!string.IsNullOrEmpty(SearchModel.Name))
                {
                    searchParams.KeyValuePairs.Add("name", SearchModel.Name);
                }

                if (!SearchModel.Gender.Equals("all"))
                {
                    searchParams.KeyValuePairs.Add("gender", SearchModel.Gender);
                }

                if (!string.IsNullOrEmpty(SearchModel.Email))
                {
                    searchParams.KeyValuePairs.Add("email", SearchModel.Email);
                }

                if (!SearchModel.Status.Equals("all"))
                {
                    searchParams.KeyValuePairs.Add("status", SearchModel.Status);
                }
                var result = await employeeService.GetEmployees(searchParams);

                SearchCompleted(result);

                (NavigateCommand as RelayCommand<string>).RaiseCanExecuteChanged();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool omitSelectionChanged = false;
        private void SetCurrentPage(int currentPage)
        {
            omitSelectionChanged = true;
            CurrentPage = currentPage;
            omitSelectionChanged = false;
        }

        private void SearchCompleted(GetEmployeeResponseModel model)
        {
            PagesLabel = $"/{model.MetaModel.PaginationModel.Pages}";
            SetCurrentPage(model.MetaModel.PaginationModel.Page);
            Total = model.MetaModel.PaginationModel.Total;
            NumberOfPages = model.MetaModel.PaginationModel.Pages;
            omitSelectionChanged = true;
            PageNumbers = Enumerable.Range(1, model.MetaModel.PaginationModel.Pages).ToList();
            omitSelectionChanged = false;
            EmployeeList = model.EmployeeList;

        }

        private List<Employee> employeeList;
        public List<Employee> EmployeeList
        {
            get => employeeList;
            set => Set(ref employeeList, value);
        }

        private int total;

        public int Total
        {
            get => total;
            set => Set(ref total, value);
        }

        private int numberOfPages;

        public int NumberOfPages
        {
            get => numberOfPages;
            set => Set(ref numberOfPages, value);
        }

        private string pagesLabel;

        public string PagesLabel
        {
            get => pagesLabel;
            set => Set(ref pagesLabel, value);
        }

        private List<int> pageNumbers;

        public List<int> PageNumbers
        {
            get => pageNumbers;
            set => Set(ref pageNumbers, value);
        }

        private int currentPage;

        public int CurrentPage
        {
            get => currentPage;
            set => Set(ref currentPage, value);
        }

        private ICommand navigateCommand;

        public ICommand NavigateCommand
        {
            get
            {
                return navigateCommand ?? (navigateCommand = new RelayCommand<string>(Navigate, CanNavigate));

                async void Navigate(string page)
                {

                    if (Int32.TryParse(page, out int pageDirection))
                    {
                        if (pageDirection != 0)
                        {
                            SetCurrentPage(CurrentPage + pageDirection);
                        }
                        if (!omitSelectionChanged)
                        {
                            await QueryEmployees();
                        }

                    }
                }


                bool CanNavigate(string page)
                {
                    int navDirection = Convert.ToInt32(page);
                    if(navDirection >= 0)
                    {
                        return NumberOfPages > CurrentPage;
                    }
                    else
                    {
                        return CurrentPage > 1;
                    }
                }
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(AddEmployee));

                async void AddEmployee()
                {
                    var employeeVm = new EmployeeViewModel(employeeService);
                    var employeeMessage = new EmployeeEditMessage
                    {
                        Employee = employeeVm,
                    };

                    MessengerInstance.Send(employeeMessage);
                    if (employeeMessage.DialogResult == true)
                        await QueryEmployees();
                }
            }
        }

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand<Employee>(EditEmployee));

               async void EditEmployee(Employee currentEmployee)
                {
                    var employeeVm = new EmployeeViewModel(employeeService)
                    {
                        Email = currentEmployee.EMail,
                        Gender = currentEmployee.Gender,
                        Id = currentEmployee.Id.ToString(),
                        Name = currentEmployee.Name,
                        Status = currentEmployee.Status,
                        ForUpdate = true
                    };
                    var employeeMessage = new EmployeeEditMessage
                    {
                        Employee = employeeVm,
                    };

                    MessengerInstance.Send(employeeMessage);
                    if (employeeMessage.DialogResult == true)
                        await QueryEmployees();

                }
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand<Employee>(DeleteEmployee));

                async void DeleteEmployee(Employee currentEmployee)
                {
                    var confirmationMessage = new ConfirmationMessage
                    {
                        Title = "Confirm Deletion",
                        Message = $"Are you sure for deleting the employee with id: ${currentEmployee.Id}?"
                    };

                    MessengerInstance.Send(confirmationMessage);
                    if (confirmationMessage.Result == true)
                    {
                        var response = await employeeService.DeleteEmployee(currentEmployee.Id);

                        if(response.IsSuccess)
                        {
                            await QueryEmployees();
                        }
                        else
                        {
                            MessengerInstance.Send(new DialogMessage
                            {
                                Title = "Error",
                                Message = $"Error occured on delete!"
                            });
                        }
                    }
                }
            }
        }
    }
}
