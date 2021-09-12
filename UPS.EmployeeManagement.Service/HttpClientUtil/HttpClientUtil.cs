using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UPS.EmployeeManagement.Model;

namespace UPS.EmployeeManagement.Service.Util
{
    public class HttpClientUtil
    {
        private readonly IRestClient _restClient;
        public HttpClientUtil(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public T Get<T>(string url, SearchParams searchParams)
        {
            string query = string.Format("{0}?{1}", url, searchParams.QueryString);
            var request = new RestRequest(query);
            IRestResponse response = _restClient.Get(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<T> GetAsync<T>(string url, SearchParams searchParams)
        {
            string query = string.Format("{0}?{1}", url, searchParams.QueryString);
            var request = new RestRequest(query);
            var result = await Task.Run(() =>
            {
                IRestResponse response = _restClient.Get(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    throw new Exception();
                }
            });

            return result;
            //string query = string.Format("{0}?{1}", url, searchParams.QueryString);
            //var request = new RestRequest(query);
            //return await _restClient.GetAsync<T>(request);
        }

        public async Task<T> PostAsync<T>(string url, T body)
        {
            var request = new RestRequest(url);
            request.AddParameter("application/json", JsonConvert.SerializeObject(body), ParameterType.RequestBody);
            
            var result = await Task.Run(() =>
            {
                IRestResponse response = _restClient.Post(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return JsonConvert.DeserializeObject<T>(response.Content);
                }
                else
                {
                    throw new Exception();
                }
            });

            return result;
        }

        public T Put<T>(string url, T body)
        {
            var request = new RestRequest(url);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = _restClient.Put(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                throw new Exception();
            }
        }

        public T Delete<T>(string url)
        {
            var request = new RestRequest(url);
            IRestResponse response = _restClient.Delete(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
