namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Employee", "Name", c => c.String(nullable: false));
            AlterColumn("public.Employee", "Photo", c => c.Binary(nullable: false));
            AlterColumn("public.Employee", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("public.Employee", "Address", c => c.String());
            AlterColumn("public.Employee", "Photo", c => c.Binary());
            AlterColumn("public.Employee", "Name", c => c.String());
        }
    }
}
