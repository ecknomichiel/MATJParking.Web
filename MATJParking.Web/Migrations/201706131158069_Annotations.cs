namespace MATJParking.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Annotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Owners", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Owners", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Owners", "LastName", c => c.String());
            AlterColumn("dbo.Owners", "FirstName", c => c.String());
        }
    }
}
