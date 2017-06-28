using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATJParking.Web.Repositories;
using MATJParking.Web.Controllers;
using MATJParking.Web.Models;
using Newtonsoft.Json;
using Telerik.JustMock;

namespace MATJParking.Web.Tests.Controllers
{
    [TestClass]
    public class VehicleControllerTest
    {
        #region Index
        [TestMethod]
        public void Index()
        {
            // Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            VehicleController controller = new VehicleController(fakeGarage);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion

        #region Search

        [TestMethod]
        public void SearchGivenDropdown1ReturnsAllParkedVehicles()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehicles()).Returns(new ParkingPlace[] { new ParkingPlace(){ID="Test", Vehicle = new Vehicle(), VehicleType = new VehicleType()}}).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "1" };
            int expectedResult = 1;
            //Act
            ViewResult result = cnt.Search(data) as ViewResult;
            int actualResult = (result.Model as SearchData).SearchResult.Count();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, actualResult);     
        }

        [TestMethod]
        public void SearchGivenDropdown2ReturnsVehiclesOnPrice()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehiclesOnPrice(10, true)).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "2", SearchValue = "10" };
            int expectedResult = 1;
            //Act
            ViewResult result = cnt.Search(data) as ViewResult;
            int actualResult = (result.Model as SearchData).SearchResult.Count();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SearchGivenDropdown3SearchValueEmptyReturnsAllVehicles()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehicles()).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "3", SearchValue = "" };
            int expectedResult = 1;
            //Act
            ViewResult result = cnt.Search(data) as ViewResult;
            int actualResult = (result.Model as SearchData).SearchResult.Count();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SearchGivenDropdown3SearchValueAReturnsVehiclesByRegNum()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchByRegNum("A")).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "3", SearchValue = "A" };
            int expectedResult = 1;
            //Act
            ViewResult result = cnt.Search(data) as ViewResult;
            int actualResult = (result.Model as SearchData).SearchResult.Count();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SearchGivenDropdown4_1ReturnsVehiclesByRegNum()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehiclesOnVehicleType(1)).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "4.1" };
            int expectedResult = 1;
            //Act
            ViewResult result = cnt.Search(data) as ViewResult;
            int actualResult = (result.Model as SearchData).SearchResult.Count();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void SearchGivenDropdown5ReturnsAllParkingPlaces()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkingPlaces()).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "5" };
            int expectedResult = 1;
            //Act
            ViewResult result = cnt.Search(data) as ViewResult;
            int actualResult = (result.Model as SearchData).SearchResult.Count();
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, actualResult);
        }
        #endregion

        #region _Search
        [TestMethod]
        public void _SearchGivenDropdown1ReturnsAllParkedVehicles()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehicles()).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "1" };
            int expectedResult = 1;
            //Act
            string result = cnt._Search(data);
            IEnumerable<ParkingPlace> model = JsonConvert.DeserializeObject<IEnumerable<ParkingPlace>>(result);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, model.Count());
        }

        [TestMethod]
        public void _SearchGivenDropdown2ReturnsVehiclesOnPrice()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehiclesOnPrice(10, true)).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "2", SearchValue = "10" };
            int expectedResult = 1;
            //Act
            string result = cnt._Search(data);
            IEnumerable<ParkingPlace> model = JsonConvert.DeserializeObject<IEnumerable<ParkingPlace>>(result);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, model.Count());
        }

        [TestMethod]
        public void _SearchGivenDropdown3SearchValueEmptyReturnsAllVehicles()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehicles()).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "3", SearchValue = "" };
            int expectedResult = 1;
            //Act
            string result = cnt._Search(data);
            IEnumerable<ParkingPlace> model = JsonConvert.DeserializeObject<IEnumerable<ParkingPlace>>(result);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, model.Count());
        }

        [TestMethod]
        public void _SearchGivenDropdown3SearchValueAReturnsVehiclesByRegNum()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchByRegNum("A")).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "3", SearchValue = "A" };
            int expectedResult = 1;
            //Act
            string result = cnt._Search(data);
            IEnumerable<ParkingPlace> model = JsonConvert.DeserializeObject<IEnumerable<ParkingPlace>>(result);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, model.Count());
        }

        [TestMethod]
        public void _SearchGivenDropdown4_1ReturnsVehiclesByRegNum()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkedVehiclesOnVehicleType(1)).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "4.1" };
            int expectedResult = 1;
            //Act
            string result = cnt._Search(data);
            IEnumerable<ParkingPlace> model = JsonConvert.DeserializeObject<IEnumerable<ParkingPlace>>(result);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, model.Count());
        }

        [TestMethod]
        public void _SearchGivenDropdown5ReturnsAllParkingPlaces()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchAllParkingPlaces()).Returns(new ParkingPlace[] { new ParkingPlace() { ID = "Test", Vehicle = new Vehicle(), VehicleType = new VehicleType() } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            SearchData data = new SearchData() { DropDown = "5" };
            int expectedResult = 1;
            //Act
            string result = cnt._Search(data);
            IEnumerable<ParkingPlace> model = JsonConvert.DeserializeObject<IEnumerable<ParkingPlace>>(result);
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedResult, model.Count());
        }
        #endregion

        #region _getVehicleTypes
        [TestMethod]
        public void _getVehicleTypesReturnsAllVehicleTypes()
        {
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.GetVehicleTypes()).Returns(new VehicleType[] { new VehicleType() { ID = 1, Name = "Viking färja", PricingFactor = 1000 } }).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            int expectedResult = 1;
            //Act
            string jsonResult = cnt._getVehicleTypes();
            IEnumerable<VehicleType> result = JsonConvert.DeserializeObject<IEnumerable<VehicleType>>(jsonResult);
            //Assert
            Assert.AreEqual(expectedResult, result.Count());
            Assert.AreEqual("Viking färja", result.First().Name);
        }
        #endregion

        #region Overview
        public void OverviewReturnsOverviewLines()
        {
            // ActionResult Overview()
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.GetOverview()).Returns(new OverviewLine[] { new OverviewLine() { VehicleType = new VehicleType(){ID = 1, Name = "Viking färja", PricingFactor = 1000 }, NumAvailablePlaces = 130 }}).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            int expectedResult = 1;
            //Act
            ViewResult viewResult = cnt.Overview() as ViewResult;
            IEnumerable<OverviewLine> result = viewResult.Model as IEnumerable<OverviewLine>;
            //Assert
            Assert.AreEqual(expectedResult, result.Count());
            Assert.AreEqual("Viking färja", result.First().VehicleType.Name);
            Assert.AreEqual(130, result.First().NumAvailablePlaces);
        }
        #endregion

        #region CarDetails
        [TestMethod]
        public void CarDetailsGivenNonExistingRegNrReturnsViewBagError()
        {
            //ActionResult CarDetails(string registrationNumber, string Id)
            //Arrange
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchPlaceWhereVehicleIsParked("NONEXIST")).Returns(null as ParkingPlace).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            ParkingPlace expectedResult = null;
            //Act
            ViewResult viewResult = cnt.CarDetails("NONEXIST", null) as ViewResult;
            ParkingPlace result = viewResult.Model as ParkingPlace;
            string actualMessage = viewResult.ViewBag.Message;
            //Assert
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual("Cannot find car with registrationnumber NONEXIST",actualMessage);
        }

        [TestMethod]
        public void CarDetailsGivenExistingRegNrReturnsNoViewBagError()
        {
            //ActionResult CarDetails(string registrationNumber, string Id)
            //Arrange
            ParkingPlace expectedResult = new ParkingPlace();
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchPlaceWhereVehicleIsParked("EXIST")).Returns(expectedResult).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            
            //Act
            ViewResult viewResult = cnt.CarDetails("EXIST", null) as ViewResult;
            ParkingPlace result = viewResult.Model as ParkingPlace;
            string actualMessage = viewResult.ViewBag.Message;
            //Assert
            Assert.AreEqual(expectedResult, result);
            Assert.AreEqual(null, actualMessage);
        }
        #endregion

        #region CheckOutYes
        [TestMethod]
        public void CheckOutYesCallsCheckOutReturnsIndex()
        {
            //Arrange
            ParkingPlace expectedResult = new ParkingPlace();
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.CheckOut("PARKED")).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            //Act
            ViewResult viewResult = cnt.CheckOutYes("PARKED") as ViewResult;
            //Assert
            Assert.AreEqual(1, Mock.GetTimesCalled(() => fakeGarage.CheckOut("PARKED")));
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [TestMethod]
        public void CheckOutYesGivenEVehicleNotFoundReturnsViewbagMessage()
        {
            //Arrange
            ParkingPlace expectedResult = new ParkingPlace();
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.CheckOut("NOTPARKED")).Throws(new EVehicleNotFound("NOTPARKED")).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            //Act
            ViewResult viewResult = cnt.CheckOutYes("NOTPARKED") as ViewResult;
            //Assert
            Assert.AreEqual("Vehicle with registration number 'NOTPARKED' not found.", viewResult.ViewBag.Message);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CheckOutYesGivenOtherExceptionThrowsException()
        {
            //Arrange
            ParkingPlace expectedResult = new ParkingPlace();
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.CheckOut("NOTPARKED")).Throws(new Exception("NOTPARKED")).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            //Act
            ActionResult viewResult = cnt.CheckOutYes("NOTPARKED");
            //Assert
            Assert.IsNotInstanceOfType(viewResult, typeof(ViewResult));
        }
        #endregion

        #region  CheckOut
        [TestMethod]
        public void CheckOutGivenParkedRegNrReturnsModelParkingPlace()
        {
            //Arrange
            ParkingPlace expectedResult = new ParkingPlace();
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchPlaceWhereVehicleIsParked("PARKED")).Returns(expectedResult).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            //Act
            ViewResult viewResult = cnt.CheckOut("PARKED") as ViewResult;
            //Assert
            Assert.AreEqual(1, Mock.GetTimesCalled(() => fakeGarage.SearchPlaceWhereVehicleIsParked("PARKED")));
            Assert.AreEqual(expectedResult, viewResult.Model as ParkingPlace);
            Assert.AreEqual("", viewResult.ViewName); //Empty ViewName means default view for the action => CheckOut
        }

        [TestMethod]
        public void CheckOutGivenNonParkedRegNrReturnsErrorMessage()
        {
            //Arrange
            ParkingPlace expectedResult = null;
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchPlaceWhereVehicleIsParked("NONPARKED")).Returns(expectedResult).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            //Act
            ViewResult viewResult = cnt.CheckOut("NONPARKED") as ViewResult;
            //Assert
            Assert.AreEqual(1, Mock.GetTimesCalled(() => fakeGarage.SearchPlaceWhereVehicleIsParked("NONPARKED")));
            Assert.AreEqual(expectedResult, viewResult.Model);
            Assert.AreEqual("Cannot find car with registrationnumber NONPARKED", viewResult.ViewBag.Message);
            Assert.AreEqual("Index", viewResult.ViewName);
        }
        #endregion

        #region CheckIn
        //ActionResult CheckIn([ModelBinder(typeof(CheckinDataModelBinder))] CheckInData checkInData)

        [TestMethod]
        public void InitialCheckInGivenExistingRegNrReturnsCheckInViewSearchDone()
        {
            //Arrange
            Vehicle expectedResult = new Vehicle() { RegNumber = "NONPARKED", VehicleType = new VehicleType() { ID = 13, Name = "Testvehicle", PricingFactor = -1 } };
            IGarage fakeGarage = Mock.Create<IGarage>();
            Mock.Arrange(() => fakeGarage.SearchPlaceWhereVehicleIsParked("NONPARKED")).Returns(null as ParkingPlace).MustBeCalled();
            Mock.Arrange(() => fakeGarage.SearchVehicle("NONPARKED")).Returns(expectedResult).MustBeCalled();
            VehicleController cnt = new VehicleController(fakeGarage);
            CheckInData data = new CheckInData();
            data.RegistrationNumber = "NONPARKED";
            //Act
            ViewResult viewResult = cnt.CheckIn(data) as ViewResult;
            CheckInData actualResult = (viewResult.Model as CheckInData);
            //Assert
            Assert.AreEqual(1, Mock.GetTimesCalled(() => fakeGarage.SearchPlaceWhereVehicleIsParked("NONPARKED")));
            Assert.AreEqual(1, Mock.GetTimesCalled(() => fakeGarage.SearchVehicle("NONPARKED")));
            Assert.IsTrue(expectedResult.Equals(actualResult.Vehicle));
            Assert.AreEqual(CheckInState.SearchDone, actualResult.State);
            Assert.AreEqual("", viewResult.ViewName);
        }
        #endregion
    }
}
