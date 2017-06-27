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

    }
}
