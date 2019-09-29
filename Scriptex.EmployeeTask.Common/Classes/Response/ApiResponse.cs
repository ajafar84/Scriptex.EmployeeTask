using Scriptex.EmployeeTask.Common.Interfaces;

namespace Scriptex.EmployeeTask.Common.Classes.Response
{
    public class ApiResponse : IResponse
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
