namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uniqueproperty : DbMigration
    {
        public override void Up()
        {
            CreateIndex("public.Login", "Username", unique: true);
            CreateIndex("public.Login", "Password", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("public.Login", new[] { "Password" });
            DropIndex("public.Login", new[] { "Username" });
        }
    }
}
