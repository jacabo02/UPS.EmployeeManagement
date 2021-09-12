using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UPS.EmployeeManagement.Model;
using UPS.EmployeeManagement.Service.Util;

namespace UPS.EmployeeManagement.Service.Impl
{
    public class EmployeeService : IEmployeeService
    {
        //private readonly ILogger _logger;
        private readonly HttpClientUtil _httpClientUtil;
        public EmployeeService(IRestClient serviceClient)
        {
            //_logger = logger;
            _httpClientUtil = new HttpClientUtil(serviceClient);
        }
        public async Task<GetEmployeeResponseModel> GetEmployees(SearchParams employeeSearchParams)//search model keyvalue pair
        {
            return await _httpClientUtil.GetAsync<GetEmployeeResponseModel>("users", employeeSearchParams);        
        }        

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            return await _httpClientUtil.PostAsync("users", employee);
        }

        public Employee UpdateEmployee(Employee employee)
        {
            return _httpClientUtil.Put("users", employee);
        }

        public ResponseBase DeleteEmployee(Employee employee)
        {
            return _httpClientUtil.Delete<ResponseBase>("users");
        }
    }
}
