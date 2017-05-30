namespace MATJParking.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MATJParking.Web.DataAccess;
    using MATJParking.Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<GarageContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GarageContext context)
        {
            //VehicleType will be filled by SeedParkingPlaces
            SeedParkingPlace(context);
            SeedVehicle(context);
        }

        private void SeedVehicle(GarageContext context)
        {

        }

        private void SeedParkingPlace(GarageContext context)
        {
            int i;
            VehicleType tp = new VehicleType() { ID = 3, Name = "Bus", PricingFactor = 4 };
            for (i = 0; i < 1; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "B" + i, VehicleType = tp });
            }
            tp = new VehicleType() { ID = 4, Name = "Truck", PricingFactor = 3 };
            for (i = 0; i < 5; i++)
            {
                //    parkingPlaces.Add(new ParkingPlace() { ID = "T" + i, VehicleType = VehicleType.Truck });
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "T" + i, VehicleType = tp });
            }
            tp = new VehicleType() { ID = 2, Name = "Car", PricingFactor = 2 };
            for (i = 0; i < 50; i++)
            {
                //    parkingPlaces.Add(new ParkingPlace() { ID = "C" + i, VehicleType = VehicleType.Car });
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "C" + i, VehicleType = tp });

            }
            tp = new VehicleType() { ID = 1, Name = "Motorcycle", PricingFactor = 1 };
            for (i = 0; i < 20; i++)
            {
                //   parkingPlaces.Add(new ParkingPlace() { ID = "M" + i, VehicleType = VehicleType.Motorcycle });
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "M" + i, VehicleType = tp });
            }
        }
    }
}
