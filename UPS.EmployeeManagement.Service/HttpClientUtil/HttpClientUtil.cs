using Flurl;
using Flurl.Http;
using System.Threading.Tasks;
using UPS.EmployeeManagement.Model;

namespace UPS.EmployeeManagement.Service.Util
{
    public class HttpClientUtil : IHttpClientUtil
    {
        private readonly IFlurlClient _restClient;
        public HttpClientUtil(IFlurlClient restClient) 
        {
            _restClient = restClient;
        }

        public HttpClientUtil()
        {
            _restClient = new FlurlClient();
        }


        public async Task<T> GetAsync<T>(string url, SearchParams searchParams)
        {
            return await _restClient
                .Request(url)
                .SetQueryParams(searchParams.QueryParams)
                .GetJsonAsync<T>();
        }

        public async Task<T> PostAsync<T>(string url, object body)
        {
            return await _restClient
                .Request(url)
                .PostJsonAsync(body)
                .ReceiveJson<T>();
        }

        public async Task<T> PutAsync<T>(string url, object body)
        {
            return await _restClient
                .Request(url)
                .PutJsonAsync(body)
                .ReceiveJson<T>();
        }

        public async Task<T> Delete<T>(string url)
        {
            return await _restClient
                .Request(url)
                .DeleteAsync()
                .ReceiveJson();
        }
    }
}
