namespace Scriptex.EmployeeTask.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeTableRefactor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("EmployeeTask.Employees", "GenderId", "EmployeeTask.Genders");
            DropIndex("EmployeeTask.Employees", new[] { "GenderId" });
            AddColumn("EmployeeTask.Employees", "Gender", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AddColumn("EmployeeTask.Employees", "CreationDate", c => c.DateTime(nullable: false, storeType: "date"));
            DropColumn("EmployeeTask.Employees", "GenderId");
            DropTable("EmployeeTask.Genders");
        }
        
        public override void Down()
        {
            CreateTable(
                "EmployeeTask.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("EmployeeTask.Employees", "GenderId", c => c.Int(nullable: false));
            DropColumn("EmployeeTask.Employees", "CreationDate");
            DropColumn("EmployeeTask.Employees", "Gender");
            CreateIndex("EmployeeTask.Employees", "GenderId");
            AddForeignKey("EmployeeTask.Employees", "GenderId", "EmployeeTask.Genders", "Id");
        }
    }
}
