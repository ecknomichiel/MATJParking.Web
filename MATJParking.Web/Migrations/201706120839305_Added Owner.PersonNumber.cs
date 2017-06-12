namespace MATJParking.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOwnerPersonNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Owners", "PersonNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Owners", "PersonNumber");
        }
    }
}
