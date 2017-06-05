using MATJParking.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MATJParking.Web.Models;
using MATJParking.Web.Repositories;
using MATJParking.Web.ModelBinder;


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
        [HttpPost]
        public ActionResult CheckIn([ModelBinder(typeof(CheckinDataModelBinder))] CheckInData checkInData)
        {

            if (checkInData.RegistrationNumber == "")
            {
                checkInData.RegistrationNumber = ViewData["registrationNumber"] as string;
            }
            switch (checkInData.State)
            {
                case CheckInState.Initial:
                    {//First time the Checkin is called: search for the car and show any details we might have
                        checkInData.Vehicle.Assign(Garage.Instance.SearchVehicle(checkInData.RegistrationNumber));
                        checkInData.State = CheckInState.SearchDone;
                        ParkingPlace pl = Garage.Instance.SearchPlaceWhereVehicleIsParked(checkInData.RegistrationNumber);
                        if (pl != null)
                        {
                            checkInData.Place = pl;
                            checkInData.State = CheckInState.AlreadyParked;
                        }
                        checkInData.VehicleTypes = Garage.Instance.GetVehicleTypes();
                        return View(checkInData);
                    }
                case CheckInState.SearchDone:
                    {
                        //Check if data is ready for checkin
                        try
                        {
                            checkInData.Place = Garage.Instance.CheckIn(checkInData);
                            checkInData.State = CheckInState.Parked;
                        }
                        catch (Exception e)
                        {
                            ViewBag.Message = e.Message;
                            if (e.GetType() == typeof(ENoPlaceForVehicle))
                            {
                                checkInData.State = CheckInState.NoPlaceForVehicle;
                            }
                            if (e.GetType() == typeof(EVehicleAlreadyCheckedIn))
                            {
                                checkInData.State = CheckInState.AlreadyParked;
                                checkInData.Place = Garage.Instance.SearchPlaceWhereVehicleIsParked(checkInData.Vehicle.RegNumber);
                            }
                        }
                        return View(checkInData);
                    }
            }
            
            //Details to be completed: do not checkin an ask to complete details
            return View(checkInData);
            
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

        public ActionResult Overview()
        {
            return View(Garage.Instance.GetOverview());
        }
    }
}