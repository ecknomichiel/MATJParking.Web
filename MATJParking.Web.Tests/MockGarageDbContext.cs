using MATJParking.Web.DataAccess;
using MATJParking.Web.Models;
using System;
using System.Collections.Generic;

using Telerik.JustMock;

namespace MATJParking.Web.Tests
{
    public class MockGarageDbContextCreator: IGarageContextCreator
    {

        public string FactoryKey
        {
            get { return "mock"; }
        }

        public IGarageContext CreateContext()
        {
            IGarageContext result = Mock.Create<IGarageContext>();
            List<VehicleType> vehicleTypes = new List<VehicleType>(){
                new VehicleType(){ID = 1, Name = "Båt", PricingFactor = 13.5},
                new VehicleType(){ID = 2, Name = "Testvolontär", PricingFactor = 0 }
            };

            List<Vehicle> vehicles = new List<Vehicle>(){
                new Vehicle(){RegNumber = "UNPARKED", VehicleType = vehicleTypes[0], Owner = new Owner(){Id = 1, FirstName = "Test", LastName = "Person", PersonNumber = "00000000-0000"}},
                new Vehicle(){RegNumber = "PARKED", VehicleType = vehicleTypes[1], Owner = new Owner(){Id = 2, FirstName = "Andra", LastName = "Person", PersonNumber = "00000000-0001"}},
            };

            List<ParkingPlace> parkingPlaces = new List<ParkingPlace>()
                {
                    new ParkingPlace {ID = "1", Vehicle = null, VehicleType = vehicleTypes[0] },
                    new ParkingPlace {ID = "2", Vehicle = vehicles[1], VehicleType = vehicleTypes[1] }
                };

            Mock.Arrange(() => result.GetAllParkingPlaces()).Returns(parkingPlaces);
            Mock.Arrange(() => result.GetVehicleByID("PARKED")).Returns(vehicles[1]);
            Mock.Arrange(() => result.GetVehicleByID("UNPARKED")).Returns(vehicles[0]);
            Mock.Arrange(() => result.GetVehicleTypes()).Returns(vehicleTypes);
            Mock.Arrange(() => result.GetVehicleTypeByID(1)).Returns(vehicleTypes[0]);
            Mock.Arrange(() => result.GetVehicleTypeByID(2)).Returns(vehicleTypes[1]);
            return result;
        }
    }

    
}
