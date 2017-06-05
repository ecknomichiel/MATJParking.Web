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
            //VehicleType will be filled by SeedParkingPlaces
            List<VehicleType> vehicleTypes = SeedVehicleType(context);
            SeedParkingPlace(context, vehicleTypes);
           // SeedVehicle(context, vehicleTypes);
        }

        private void SeedVehicle(GarageContext context, List<VehicleType> vtps)
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
                VehicleType = vtps[1]
            }
                );
        }

        private List<VehicleType> SeedVehicleType(GarageContext context)
        {
            List<VehicleType> result = new List<VehicleType>()
            {
                new VehicleType() { ID = 3, Name = "Bus", PricingFactor = 4 },
                new VehicleType() { ID = 4, Name = "Truck", PricingFactor = 3 },
                new VehicleType() { ID = 2, Name = "Car", PricingFactor = 2 },
                new VehicleType() { ID = 1, Name = "Motorcycle", PricingFactor = 1 }
            };

            foreach (VehicleType vehicleType in result)
            {
                context.VehicleTypes.AddOrUpdate(vt => vt.ID, vehicleType);
            }

            return result;
        }

        private void SeedParkingPlace(GarageContext context, List<VehicleType> vehicleTypes)
        {
            int i;
            VehicleType tp = vehicleTypes[0];
            for (i = 0; i < 1; i++)
            {
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "B" + i, VehicleType = tp });
            }
            tp = vehicleTypes[1];
            for (i = 0; i < 5; i++)
            {
                //    parkingPlaces.Add(new ParkingPlace() { ID = "T" + i, VehicleType = VehicleType.Truck });
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "T" + i, VehicleType = tp });
            }
            tp = vehicleTypes[2];
            for (i = 0; i < 50; i++)
            {
                //    parkingPlaces.Add(new ParkingPlace() { ID = "C" + i, VehicleType = VehicleType.Car });
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "C" + i, VehicleType = tp });

            }
            tp = vehicleTypes[3];
            for (i = 0; i < 20; i++)
            {
                //   parkingPlaces.Add(new ParkingPlace() { ID = "M" + i, VehicleType = VehicleType.Motorcycle });
                context.ParkingPlaces.AddOrUpdate(pp => pp.ID, new ParkingPlace() { ID = "M" + i, VehicleType = tp });
            }
        }
    }
}
