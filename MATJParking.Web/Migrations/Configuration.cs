namespace MATJParking.Web.Migrations
{
    using System;
    using System.Collections.Generic;
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
            SeedVehicleType(context);
            SeedParkingPlace(context);
            SeedVehicle(context);
        }

        private void SeedVehicle(GarageContext context)
        {
            context.Vehicles.AddOrUpdate(v => v.RegNumber, new Vehicle()
            {
                RegNumber = "AAA 123",
                CheckInTime = DateTime.Now,
                Owner = new Owner()
                {
                    LastName = "Mylastname",
                    FirstName = "Myname",
                    Id = 1
                },
                VehicleType = context.GetVehicleTypeByID(2)
            }
                );
        }

        private void SeedVehicleType(GarageContext context)
        {
            List<VehicleType> result = new List<VehicleType>()
            {
                new VehicleType() { ID = 3, Name = "Bus", PricingFactor = 4 },
                new VehicleType() { ID = 4, Name = "Truck", PricingFactor = 3 },
                new VehicleType() { ID = 2, Name = "Car", PricingFactor = 2 },
                new VehicleType() { ID = 1, Name = "Motorcycle", PricingFactor = 1 },
                new VehicleType() { ID = 5, Name = "Horse and cart", PricingFactor = 2.5 },
                new VehicleType() { ID = 6, Name = "Stridsvagn", PricingFactor = 6 },
                new VehicleType() { ID = 7, Name = "Helikopter", PricingFactor = 8 } 
            };

            foreach (VehicleType vehicleType in result)
            {
                context.VehicleTypes.AddOrUpdate(vt => vt.ID, vehicleType);
            }
            context.SaveChanges();
        }

        private void SeedParkingPlace(GarageContext context)
        {
            int i;
            VehicleType tp = context.GetVehicleTypeByID(3);
            for (i = 0; i < 1; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "B" + i, VehicleType = tp });
            }
            tp = context.GetVehicleTypeByID(4);
            for (i = 0; i < 5; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "T" + i, VehicleType = tp });
            }
            tp = context.GetVehicleTypeByID(2);
            for (i = 0; i < 50; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "C" + i, VehicleType = tp });

            }
            tp = context.GetVehicleTypeByID(1);
            for (i = 0; i < 20; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "M" + i, VehicleType = tp });
            }
            tp = context.GetVehicleTypeByID(5);
            for (i = 0; i < 2; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "H" + i, VehicleType = tp });
            }
            tp = context.GetVehicleTypeByID(6);
            for (i = 0; i < 2; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "S" + i, VehicleType = tp });
            }
            tp = context.GetVehicleTypeByID(7);
            for (i = 0; i < 8; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "K" + i, VehicleType = tp });
            }
        }
    }
}
