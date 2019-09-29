using Scriptex.EmployeeTask.Data.ViewModels.Job;
using System.Collections.Generic;

namespace Scriptex.EmployeeTask.Data.ViewModels.Employee.Page
{
    public class EmployeeEditPageVM
    {
        public EmployeeVM EmployeeVM { get; set; }
        public List<JobVM> JobsVM { get; set; }
    }
}
