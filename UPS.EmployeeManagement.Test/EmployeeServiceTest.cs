using NSubstitute;
using RestSharp;
using System;
using UPS.EmployeeManagement.Model;
using UPS.EmployeeManagement.Service;
using UPS.EmployeeManagement.Service.Impl;
using UPS.EmployeeManagement.Test.MockData;
using Xunit;

namespace UPS.EmployeeManagement.Test
{
    public class EmployeeServiceTest
    {

        private readonly IEmployeeService _employeeService;
        private IRestClient _serviceClient;
        public EmployeeServiceTest()
        {
            _serviceClient = Substitute.For<IRestClient>();            
            _employeeService = new EmployeeService(_serviceClient);
        }


        [Theory]
        [InlineData(null, null)]
        [InlineData("moq", null)]
        [InlineData(null, "moq")]
        [InlineData(null, "")]
        [InlineData("", null)]        
        [InlineData("", "moq")]
        [InlineData("moq", "")]
        [InlineData("", "")]
        public async void GetEmployees_When_ParametersIsEmpty_Then_ShouldReturnAllEmployees(string searchKey, string searchValue)
        {
            _serviceClient.Get(Arg.Any<IRestRequest>()).Returns(RestClientMock.GetEmployees_RestResponseMock());
            GetEmployeeResponseModel expectedModel = GetEmployeeResponseModelMock.GetEmployeeResponseModel();
            //_employeeService.GetEmployees(searchKey, searchValue).Returns(GetEmployeeResponseModelMock.GetEmployeeResponseModel());

            GetEmployeeResponseModel resultModel = await _employeeService.GetEmployees(new SearchParams());

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
    }
}
