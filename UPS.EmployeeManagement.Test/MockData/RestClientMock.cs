using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using UPS.EmployeeManagement.Model;

namespace UPS.EmployeeManagement.Test.MockData
{
    public static class RestClientMock
    {
        public static IRestClient GetRestClient()
        {
            IRestClient client = new RestClient();
            client.BaseUrl = new Uri("https://gorest.co.in/public-api/");
            client.Timeout = -1;
            client.AddDefaultHeader("Authorization", "Bearer fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
            client.AddDefaultHeader("Content-Type", "application/json");

            return client;
        }

        public static IRestRequest GetEmployees_RestRequestMock(string searchKey, string searchValue)
        {
            string queryString = !string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(searchValue)
                ? string.Format("{0}?{1}={2}", "users", searchKey, searchValue) : string.Empty;

            var request = new RestRequest(queryString);

            return request;
        }

        public static IRestResponse GetEmployees_RestResponseMock()
        {
            GetEmployeeResponseModel mockResponseObject = GetEmployeeResponseModelMock.GetEmployeeResponseModel();

            var response = new RestResponse();
            response.Content = JsonConvert.SerializeObject(mockResponseObject);
            response.StatusCode = System.Net.HttpStatusCode.OK;

            return response;
        }
    }
}
