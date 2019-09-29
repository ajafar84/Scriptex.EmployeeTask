namespace Scriptex.EmployeeTask.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeTableRefactor1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("EmployeeTask.Employees", "Name", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("EmployeeTask.Employees", "Email", c => c.String(nullable: false, maxLength: 254));
            AlterColumn("EmployeeTask.Employees", "Mobile", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("EmployeeTask.Employees", "NationalId", c => c.String(nullable: false, maxLength: 14));
            AlterColumn("EmployeeTask.Employees", "Gender", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("EmployeeTask.Employees", "Gender", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("EmployeeTask.Employees", "NationalId", c => c.String(maxLength: 14));
            AlterColumn("EmployeeTask.Employees", "Mobile", c => c.String(maxLength: 11));
            AlterColumn("EmployeeTask.Employees", "Email", c => c.String(maxLength: 254));
            AlterColumn("EmployeeTask.Employees", "Name", c => c.String(maxLength: 70));
        }
    }
}
