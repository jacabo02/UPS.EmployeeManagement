using UPS.EmployeeManagement.Model;

namespace UPS.EmployeeManagement.Test.MockData
{
    public static class GetEmployeeResponseModelMock
    {
        public static GetEmployeeResponseModel GetEmployeeResponseModel()
        {
            return new GetEmployeeResponseModel
            {
                Code = System.Net.HttpStatusCode.OK,
                EmployeeList = new System.Collections.Generic.List<Employee>()
                {
                    new Employee()
                    {
                        EMail = "hdurmus02@gmail.com",
                        Gender = "male",
                        Id = 123,
                        Message = string.Empty,
                        Name = "Hasan Durmuş",
                        Status = "active"
                    }
                },
                MetaModel = new ResponseMetaModel
                {
                    PaginationModel = new PaginationModel
                    {
                        Limit = 20,
                        Page = 1,
                        Pages = 1,
                        Total = 1
                    }
                }
            };
        }
    }

}