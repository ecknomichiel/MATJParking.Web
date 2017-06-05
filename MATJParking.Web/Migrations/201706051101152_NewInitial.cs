namespace MATJParking.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkingPlaces",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Vehicle_RegNumber = c.String(maxLength: 128),
                        VehicleType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Vehicles", t => t.Vehicle_RegNumber)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_ID)
                .Index(t => t.Vehicle_RegNumber)
                .Index(t => t.VehicleType_ID);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        RegNumber = c.String(nullable: false, maxLength: 128),
                        CheckInTime = c.DateTime(nullable: false),
                        Owner_Id = c.Int(),
                        VehicleType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.RegNumber)
                .ForeignKey("dbo.Owners", t => t.Owner_Id)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleType_ID)
                .Index(t => t.Owner_Id)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkingPlaces", "VehicleType_ID", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkingPlaces", "Vehicle_RegNumber", "dbo.Vehicles");
            DropForeignKey("dbo.Vehicles", "VehicleType_ID", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "Owner_Id", "dbo.Owners");
            DropIndex("dbo.Vehicles", new[] { "VehicleType_ID" });
            DropIndex("dbo.Vehicles", new[] { "Owner_Id" });
            DropIndex("dbo.ParkingPlaces", new[] { "VehicleType_ID" });
            DropIndex("dbo.ParkingPlaces", new[] { "Vehicle_RegNumber" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.ParkingPlaces");
            DropTable("dbo.Owners");
        }
    }
}
