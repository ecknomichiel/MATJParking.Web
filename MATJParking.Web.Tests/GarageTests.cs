using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATJParking.Web.Repositories;
using MATJParking.Web.Models;
using MATJParking.Web.DataAccess; // For GarageContextFactory

namespace MATJParking.Web.Tests
{
    [TestClass]
    public class GarageTests
    {
        /* 
         * 
         * Testdata is created in MockGarageDbContext using JustMock.
         * 
         * 
        ParkingPlace CheckIn(CheckInData data);
        void CheckOut(string RegistrationNumber);

         * IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnParkingTime(double hours, bool greaterThan);
        Vehicle SearchVehicle(string aRegistrationNumber);
        VehicleType GetVehicleType(int aVehicleTypeId);
        ParkingPlace SearchPlaceWhereVehicleIsParked(string aRegistrationNumber);
        IEnumerable<VehicleType> GetVehicleTypes();

        IEnumerable<ParkingPlace> SearchByRegNum(string aRegistrationNumber);
        IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnVehicleType(int vehicleTypeId);
         */
        public GarageTests()
        { //Because MockGarageDbContextCreator is in a different assembly, it is not found automatically
            GarageContextFactory.Instance.Register("", new MockGarageDbContextCreator());
            GarageContextFactory.Instance.Register("mock", new MockGarageDbContextCreator());
        }

        [TestMethod]
        public void SearchAllParkIngPlacesReturns2()
        {
            //Arrange: Testdata is created in MockGarageDbContext using JustMock.

            //Act
            IEnumerable<ParkingPlace> actualResult = Garage.Instance.SearchAllParkingPlaces();
            //Assert
            Assert.AreEqual(2, actualResult.Count());
        }

        [TestMethod]
        public void SearchAllParkedVehiclesReturns1()
        {
            //Arrange: Testdata is created in MockGarageDbContext using JustMock.

            //Act
            IEnumerable<ParkingPlace> actualResult = Garage.Instance.SearchAllParkedVehicles();
            //Assert
            Assert.AreEqual(1, actualResult.Count());
        }

        [TestMethod]
        public void GetOverViewReturns2()
        {
            //Arrange: Testdata is created in MockGarageDbContext using JustMock.

            //Act
            IEnumerable<OverviewLine> actualResult = Garage.Instance.GetOverview();
            //Assert
            Assert.AreEqual(2, actualResult.Count());
            Assert.AreEqual(1, actualResult.First().NumAvailablePlaces);
            Assert.AreEqual(0, actualResult.Last().NumAvailablePlaces);
        }

        [TestMethod]
        public void SearchVehiclesOnparkingTimeGiven1GreaterReturns0()
        {
            //Arrange: Testdata is created in MockGarageDbContext using JustMock.

            //Act
            IEnumerable<ParkingPlace> actualResult = Garage.Instance.SearchAllParkedVehiclesOnParkingTime(1, true);
            //Assert
            Assert.AreEqual(1, actualResult.Count());
            foreach(ParkingPlace item in actualResult)
            {
                Assert.IsTrue(item.ParkingTime >= 1);
            }
        }

        [TestMethod]
        public void SearchVehiclesOnparkingTimeGiven1LesserReturns1()
        {
            //Arrange: Testdata is created in MockGarageDbContext using JustMock.

            //Act
            IEnumerable<ParkingPlace> actualResult = Garage.Instance.SearchAllParkedVehiclesOnParkingTime(1, false);
            //Assert
            Assert.AreEqual(0, actualResult.Count());
            foreach (ParkingPlace item in actualResult)
            {
                Assert.IsTrue(item.ParkingTime <= 1);
            }
        }
    }
}
