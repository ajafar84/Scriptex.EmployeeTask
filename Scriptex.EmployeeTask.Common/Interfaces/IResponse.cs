using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptex.EmployeeTask.Common.Interfaces
{
    public interface IResponse
    {
        bool IsSuccess { get; set; }
        object Data { get; set; }
    }
}
