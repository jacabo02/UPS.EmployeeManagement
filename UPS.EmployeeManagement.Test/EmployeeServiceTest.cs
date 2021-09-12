using Flurl.Http;
using Flurl.Http.Testing;
using NSubstitute;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPS.EmployeeManagement.Model;
using UPS.EmployeeManagement.Service;
using UPS.EmployeeManagement.Service.Impl;
using UPS.EmployeeManagement.Service.Util;
using UPS.EmployeeManagement.Test.MockData;
using Xunit;

namespace UPS.EmployeeManagement.Test
{
    public class EmployeeServiceTest
    {

        private IEmployeeService _employeeService;
        private IFlurlClient _serviceClient;
        private readonly ILogger _logger;
        private HttpTest _httpTest;
        public EmployeeServiceTest()
        {
            _serviceClient = Substitute.For<IFlurlClient>();
            _httpTest = new HttpTest();
            _logger = Substitute.For<ILogger>();             
        }


        [Fact]
        public void GetEmployees_When_ParametersIsEmpty_Then_ShouldReturnAllEmployees()
        {
            try
            {
                
                //var retResponseMock = RestClientMock.GetEmployees_RestResponseMock();
                var expectedModel = GetEmployeeResponseModelMock.GetEmployeeResponseModel();
                
                _serviceClient.BaseUrl = "https://gorest.co.in/public-api/";
                _serviceClient.WithHeader("Authorization", "Bearer fa114107311259f5f33e70a5d85de34a2499b4401da069af0b1d835cd5ec0d56");
                _serviceClient.WithHeader("Content-Type", "application/json");


                //_serviceClient
                //.Request(Arg.Any<string>())
                ////.SetQueryParams(Arg.Any<IEnumerable<string>>())
                //.GetJsonAsync<GetEmployeeResponseModel>()
                //.ReturnsForAnyArgs<GetEmployeeResponseModel>(expectedModel);

                //_serviceClient
                //    .Request(Arg.Any<string>())
                //    .GetJsonAsync<GetEmployeeResponseModel>()
                //    .Returns(expectedModel);
                var clientUtilMock = Substitute.For<IHttpClientUtil>();
                
                clientUtilMock
                    .GetAsync<GetEmployeeResponseModel>(Arg.Any<string>(), Arg.Any<SearchParams>())
                    .Returns(expectedModel);

                _employeeService = new EmployeeService(_serviceClient, _logger);
                GetEmployeeResponseModel resultModel = _employeeService.GetEmployees(new SearchParams()).Result;

                Assert.NotNull(resultModel);
                Assert.Equal(expectedModel.Code, resultModel.Code);
                Assert.NotNull(resultModel.EmployeeList);
                Assert.Equal(expectedModel.EmployeeList.Count, resultModel.EmployeeList.Count);
                Assert.NotNull(expectedModel.MetaModel);
                Assert.NotNull(expectedModel.MetaModel.PaginationModel);
                Assert.Equal(expectedModel.MetaModel.PaginationModel.Limit, resultModel.MetaModel.PaginationModel.Limit);
                Assert.Equal(expectedModel.MetaModel.PaginationModel.Page, resultModel.MetaModel.PaginationModel.Page);
                Assert.Equal(expectedModel.MetaModel.PaginationModel.Pages, resultModel.MetaModel.PaginationModel.Pages);
                Assert.Equal(expectedModel.MetaModel.PaginationModel.Total, resultModel.MetaModel.PaginationModel.Total);
            }
            catch (Exception exc)
            {

                throw;
            }
            

            
        }
    }
}
