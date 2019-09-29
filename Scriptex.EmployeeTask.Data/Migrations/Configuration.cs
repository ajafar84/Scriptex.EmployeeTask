using Scriptex.EmployeeTask.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptex.EmployeeTask.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<EmployeeTaskContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
