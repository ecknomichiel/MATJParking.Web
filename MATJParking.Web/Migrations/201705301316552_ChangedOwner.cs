namespace MATJParking.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedOwner : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Owners");
            AddColumn("dbo.Owners", "CustomerID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Owners", "FirstName", c => c.String());
            AddColumn("dbo.Owners", "Phone", c => c.String());
            AddPrimaryKey("dbo.Owners", "CustomerID");
            DropColumn("dbo.Owners", "CustommerID");
            DropColumn("dbo.Owners", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Owners", "Name", c => c.String());
            AddColumn("dbo.Owners", "CustommerID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Owners");
            DropColumn("dbo.Owners", "Phone");
            DropColumn("dbo.Owners", "FirstName");
            DropColumn("dbo.Owners", "CustomerID");
            AddPrimaryKey("dbo.Owners", "CustommerID");
        }
    }
}
