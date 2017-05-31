using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MATJParking.Web;
using MATJParking.Web.Controllers;

namespace MATJParking.Web.Tests.Controllers
{
    [TestClass]
    public class VehicleControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            VehicleController controller = new VehicleController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
