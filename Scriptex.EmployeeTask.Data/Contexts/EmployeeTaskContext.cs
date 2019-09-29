using Scriptex.EmployeeTask.Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Scriptex.EmployeeTask.Data.Contexts
{
    public class EmployeeTaskContext : DbContext
    {
        public EmployeeTaskContext() : base("name=EmployeeTaskContext")
        {
            //Disable Initializer
            Database.SetInitializer<EmployeeTaskContext>(null);

            //Disable Lazy Loading
            Configuration.LazyLoadingEnabled = false;

            //Set Timeout to 3 minutes
            Database.CommandTimeout = 180;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Disable Cascade Delete for All Tables Relation
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public virtual DbSet<Models.Employee> Employees { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
    }
}
