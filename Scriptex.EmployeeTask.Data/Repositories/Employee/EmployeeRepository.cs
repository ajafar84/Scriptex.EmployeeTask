﻿using Scriptex.EmployeeTask.Common.Repositories;
using Scriptex.EmployeeTask.Data.Contexts;
using Scriptex.EmployeeTask.Data.Models;

namespace Scriptex.EmployeeTask.Data.Repositories
{
    public class EmployeeRepository : Repository<EmployeeTaskContext, Models.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeTaskContext context) : base(context)
        {
        }

        public EmployeeTaskContext EmployeeTaskContext
        {
            get { return _context as EmployeeTaskContext; }
        }
    }
}
