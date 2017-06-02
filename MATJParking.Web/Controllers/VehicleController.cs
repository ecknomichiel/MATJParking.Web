using MATJParking.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MATJParking.Web.Models;
using MATJParking.Web.Repositories;


namespace MATJParking.Web.Controllers
{
    public class VehicleController : Controller
    {
        public VehicleController() {}
    
        // GET: Vehicle
        public ActionResult Index()
        {
            return View(Garage.Instance.GetVehicleTypes());
        }
        [HttpGet]
        public ActionResult CheckIn(string registrationNumber)
        {
            //First time the Checkin is called: search for the car and show any details we might have
            CheckInState state = new CheckInState() { RegistrationNumber = registrationNumber };
            state.Vehicle = Garage.Instance.SearchVehicle(state.RegistrationNumber);
                  
            state.Place = Garage.Instance.SearchPlaceWhereVehicleIsParked(state.RegistrationNumber);
            if (state.Place != null)
            {
                ViewBag.Message = "The car is already parked at place " + state.Place.ID;
            }
            return View(state);
        }

        [HttpPost]
        public ActionResult CheckIn([Bind(Include = "registrationNumber,vehicleTypeId")] CheckInState state  )
        {
            //Read all data from the form into the objects
            state.Vehicle = Garage.Instance.SearchVehicle(state.RegistrationNumber);
            if (state.Vehicle == null)
                state.Vehicle = Garage.Instance.CreateVehicle(state.VehicleTypeId);
            state.Vehicle.RegNumber = state.RegistrationNumber;

            
            //Check if data is ready for checking
            if (state.ReadyToCheckin)
            {//Check in
                state.Place = Garage.Instance.CheckIn("registrationNumber", state.VehicleTypeId);
                return View(state);
            }

            //Details to be completed: do not checkin an ask to complete details
            ViewBag.Message = "Please add missing data.";
            return View(state);
            
        }
        // CheckOut                               
        public ActionResult CheckOut(string RegistrationNumber)
        {
            ParkingPlace pl = Garage.Instance.SearchPlaceWhereVehicleIsParked(RegistrationNumber);

            return View(pl);
        }
        public ActionResult Yes(string VehicleRegNumber)
        {
            Garage.Instance.CheckOut(VehicleRegNumber);

            return RedirectToAction("Index");
        }			
        public ActionResult SearchCar(string registrationNumber)
        {
            ParkingPlace pl = Garage.Instance.SearchPlaceWhereVehicleIsParked(registrationNumber);
            if (pl == null)
            {
                ViewBag.Message = "Cannot find car with registrationnumber " + registrationNumber;
            }
            return View(pl);
        }
    }
}