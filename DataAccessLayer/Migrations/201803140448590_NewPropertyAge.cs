namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPropertyAge : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Employee", "Age", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Employee", "Age");
        }
    }
}
