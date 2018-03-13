namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Employee", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.Employee", "Name");
        }
    }
}
