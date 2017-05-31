using MATJParking.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MATJParking.Web.Models;

namespace MATJParking.Web.Controllers
{
    public class VehicleController : Controller
    {          
                
         GarageContext context = null;

        public VehicleController()
         {
           
         }
    
        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

          // Create
        [HttpGet]
        public ActionResult CheckIn(string RegistrationNumber, Vehicle aVehicleType)
        {
            return View();  // 
        }
        [HttpPost]
        public ActionResult CheckIn(string RegistrationNumber, Vehicle aVehicleType)
        {
                   
            context.AddVehicle(aVehicleType);
            return RedirectToAction("Index");
        }



    }

}
