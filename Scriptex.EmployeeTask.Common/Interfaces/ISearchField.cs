using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptex.EmployeeTask.Common.Interfaces
{
    public interface ISearchField
    {
        string FieldName { get; set; }
        string Operator { get; set; }
        string Value { get; set; }
    }
}
