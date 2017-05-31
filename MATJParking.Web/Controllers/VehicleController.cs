using MATJParking.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MATJParking.Web.Models;
using MATJParking.Web.Repositories;
using MATJParking.Web.Models;

namespace MATJParking.Web.Controllers
{
    public class VehicleController : Controller
    {
        public VehicleController()
         {
           
         }
    
        // GET: Vehicle
        public ActionResult Index()
        {
            return View(Garage.Instance.GetVehicleTypes());
        }

        public ActionResult CheckIn(string registrationNumber, int? vehicleTypeId)
        {
            Vehicle vehicle = Garage.Instance.SearchVehicle(registrationNumber);
            ParkingPlace pl = null;

            if (vehicleTypeId == null)
            {
                ViewBag.Message = "Vehicle type is required";
                return RedirectToAction("Index");
            }

            try
            {
                pl = Garage.Instance.CheckIn(registrationNumber, vehicleTypeId.Value);
            }
            catch (Exception e)
            {
                //No space available or vehicle already parked
                ViewBag.Message = e.Message;
                //Add a dummy parkingplace with all available vehicle info
                pl = new ParkingPlace();
                if (vehicle != null)
                    pl.Park(vehicle);
            }

            return View(pl);
        }
    }

}