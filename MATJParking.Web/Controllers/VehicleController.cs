using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MATJParking.Web.Repositories;
using MATJParking.Web.Models;

namespace MATJParking.Web.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckIn(string registrationNumber, string vehicleTypeId)
        {
            int type=0;
            if ("" != vehicleTypeId)
                type = int.Parse(vehicleTypeId);
            Vehicle vehicle = Garage.Instance.SearchVehicle(registrationNumber); 
            ParkingPlace pl = null;

            try 
	        {	        
		        pl = Garage.Instance.CheckIn(registrationNumber, type);
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