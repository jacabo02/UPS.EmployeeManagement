using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPS.EmployeeManagement.Model;

namespace UPS.EmployeeManagement.Service
{
    public interface IEmployeeService
    {
        Task<dynamic> GetEmployees(SearchParams employeeSearchParams);

        Task<CreateEmployeeResponse> CreateEmployee(Employee employee);

        Task<ResponseBase> DeleteEmployee(int id);

        Task<CreateEmployeeResponse> UpdateEmployee(Employee employee);
    }
}
