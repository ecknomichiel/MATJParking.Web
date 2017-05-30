namespace MATJParking.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.ParkingPlaces",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        VehicleType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_ID)
                .Index(t => t.VehicleType_ID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PricingFactor = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        RegNumber = c.String(nullable: false, maxLength: 128),
                        CheckInTime = c.DateTime(nullable: false),
                        CheckOutTime = c.DateTime(nullable: false),
                        VehicleType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.RegNumber)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_ID)
                .Index(t => t.VehicleType_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "VehicleType_ID", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkingPlaces", "VehicleType_ID", "dbo.VehicleTypes");
            DropIndex("dbo.Vehicles", new[] { "VehicleType_ID" });
            DropIndex("dbo.ParkingPlaces", new[] { "VehicleType_ID" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.ParkingPlaces");
            DropTable("dbo.Owners");
        }
    }
}
