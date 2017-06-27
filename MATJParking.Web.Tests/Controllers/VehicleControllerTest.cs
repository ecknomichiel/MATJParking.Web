using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATJParking.Web.Repositories;
using MATJParking.Web.Controllers;
using MATJParking.Web.Models;
using Telerik.JustMock;

namespace MATJParking.Web.Tests.Controllers
{
    [TestClass]
    public class VehicleControllerTest
    {
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
    }
}
