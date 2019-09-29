using Scriptex.EmployeeTask.Common.Enums;
using Scriptex.EmployeeTask.Common.Interfaces;

namespace Scriptex.EmployeeTask.Common.Classes.Response
{
    public class ServiceResponse : IResponse
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public FailReason FailReason { get; set; }
    }
}
