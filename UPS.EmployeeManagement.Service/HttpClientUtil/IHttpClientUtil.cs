using System.Threading.Tasks;
using UPS.EmployeeManagement.Model;

namespace UPS.EmployeeManagement.Service.Util
{
    public interface IHttpClientUtil
    {
        Task<T> GetAsync<T>(string url, SearchParams searchParams);
        Task<Tout> PostAsync<Tout>(string url, object body);
        Task<T> PutAsync<T>(string url, object body);
        Task<T> Delete<T>(string url);
    }
}
