namespace MATJParking.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Owner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Owner_CustomerID", c => c.Int());
            CreateIndex("dbo.Vehicles", "Owner_CustomerID");
            AddForeignKey("dbo.Vehicles", "Owner_CustomerID", "dbo.Owners", "CustomerID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Owner_CustomerID", "dbo.Owners");
            DropIndex("dbo.Vehicles", new[] { "Owner_CustomerID" });
            DropColumn("dbo.Vehicles", "Owner_CustomerID");
        }
    }
}
