namespace Scriptex.EmployeeTask.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeRefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("EmployeeTask.Employees", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("EmployeeTask.Jobs", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("EmployeeTask.Employees", "Mobile", c => c.String(nullable: false));
            AlterColumn("EmployeeTask.Employees", "NationalId", c => c.String(nullable: false));
            DropColumn("EmployeeTask.Employees", "IsInactive");
            DropColumn("EmployeeTask.Jobs", "IsInactive");
        }
        
        public override void Down()
        {
            AddColumn("EmployeeTask.Jobs", "IsInactive", c => c.Boolean(nullable: false));
            AddColumn("EmployeeTask.Employees", "IsInactive", c => c.Boolean(nullable: false));
            AlterColumn("EmployeeTask.Employees", "NationalId", c => c.String(nullable: false, maxLength: 14));
            AlterColumn("EmployeeTask.Employees", "Mobile", c => c.String(nullable: false, maxLength: 11));
            DropColumn("EmployeeTask.Jobs", "IsActive");
            DropColumn("EmployeeTask.Employees", "IsActive");
        }
    }
}
