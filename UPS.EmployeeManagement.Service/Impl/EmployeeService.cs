using Flurl.Http;
using Serilog;
using System.Threading.Tasks;
using UPS.EmployeeManagement.Model;
using UPS.EmployeeManagement.Service.Util;

namespace UPS.EmployeeManagement.Service.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHttpClientUtil _httpClientUtil;
        private readonly ILogger _logger; 
        public EmployeeService(IFlurlClient serviceClient, ILogger logger)
        {
            _logger = logger.ForContext(GetType()); ;
            _httpClientUtil = new HttpClientUtil(serviceClient);
        }
        public async Task<GetEmployeeResponseModel> GetEmployees(SearchParams employeeSearchParams)
        {
            _logger.Information("{@SearchParams}", employeeSearchParams);
            return await _httpClientUtil.GetAsync<GetEmployeeResponseModel>("users", employeeSearchParams);
        }        

        public async Task<CreateEmployeeResponse> CreateEmployee(Employee employee)
        {
            return await _httpClientUtil.PostAsync<CreateEmployeeResponse>("users", employee);
        }

        public async Task<CreateEmployeeResponse> UpdateEmployee(Employee employee)
        {
            return await _httpClientUtil.PutAsync<CreateEmployeeResponse>(string.Format("users/{0}", employee.Id), employee);
        }

        public async Task<ResponseBase> DeleteEmployee(int id)
        {
            return await _httpClientUtil.Delete<ResponseBase>(string.Format("users/{0}", id));
        }
    }
}
