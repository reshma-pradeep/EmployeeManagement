namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewLockProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Employee", "Attempts", c => c.Int(nullable: false));
            AddColumn("public.Employee", "IsLocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("public.Employee", "IsLocked");
            DropColumn("public.Employee", "Attempts");
        }
    }
}
