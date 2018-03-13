namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "public.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeCode = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateOfJoining = c.DateTime(nullable: false),
                        Photo = c.Binary(),
                        Address = c.String(),
                        MobileNumber = c.Long(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("public.Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "public.Login",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.Long(nullable: false),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Employee", "DepartmentId", "public.Department");
            DropIndex("public.Employee", new[] { "DepartmentId" });
            DropTable("public.Login");
            DropTable("public.Employee");
            DropTable("public.Department");
        }
    }
}
