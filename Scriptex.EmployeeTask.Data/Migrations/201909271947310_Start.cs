namespace Scriptex.EmployeeTask.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "EmployeeTask.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 70),
                        JobId = c.Int(nullable: false),
                        Email = c.String(maxLength: 254),
                        Mobile = c.String(maxLength: 11),
                        NationalId = c.String(maxLength: 14),
                        GenderId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("EmployeeTask.Genders", t => t.GenderId)
                .ForeignKey("EmployeeTask.Jobs", t => t.JobId)
                .Index(t => t.JobId)
                .Index(t => t.GenderId);
            
            CreateTable(
                "EmployeeTask.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "EmployeeTask.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("EmployeeTask.Employees", "JobId", "EmployeeTask.Jobs");
            DropForeignKey("EmployeeTask.Employees", "GenderId", "EmployeeTask.Genders");
            DropIndex("EmployeeTask.Employees", new[] { "GenderId" });
            DropIndex("EmployeeTask.Employees", new[] { "JobId" });
            DropTable("EmployeeTask.Jobs");
            DropTable("EmployeeTask.Genders");
            DropTable("EmployeeTask.Employees");
        }
    }
}
