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
        public ActionResult Index(string id="")
        {
            ViewBag.Message = id;
            return View(Garage.Instance.GetVehicleTypes());
        }
        [HttpPost]
        public ActionResult CheckIn([ModelBinder(typeof(CheckinDataModelBinder))] CheckInData checkInData)
        {

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
                                //Load miniml data for displying error page: Vehicle type
                                checkInData.Vehicle.VehicleType = Garage.Instance.GetVehicleType(checkInData.VehicleTypeId);
                            }
                            if (e.GetType() == typeof(EVehicleAlreadyCheckedIn))
                            {
                                checkInData.State = CheckInState.AlreadyParked;
                                //Load data for displying error page: Parking place
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
            if (RegistrationNumber == null)
                return RedirectToAction("Index");
            ParkingPlace pl = Garage.Instance.SearchPlaceWhereVehicleIsParked(RegistrationNumber);
            if (pl == null)
            {
                string msg = "Cannot find car with registrationnumber " + RegistrationNumber;
                return RedirectToAction("Index", new {id = msg});
            }
            return View(pl);
        }


        public ActionResult CheckOutYes(string VehicleRegNumber)
        {
            try
            {
                Garage.Instance.CheckOut(VehicleRegNumber);
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
            //If everything went OK, go back to the index page
            return RedirectToAction("Index");
        }			
        public ActionResult CarDetails(string registrationNumber, string Id)
        {
            ParkingPlace pl = null;
            if (registrationNumber == null)
            {
                registrationNumber = Id;
            }
            if (registrationNumber != null)
            {
                pl = Garage.Instance.SearchPlaceWhereVehicleIsParked(registrationNumber);
            }

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

        public IEnumerable<ParkingPlace> _Search(SearchData data)
        {
            if (data == null)
            {
                data = new SearchData();
            }

            switch (data.MenuOption)
            {
                case '1':
                    data.SearchResult = Garage.Instance.SearchAllParkedVehicles();
                    break;
                case '2':
                    double sv;
                    double.TryParse(data.SearchValue, out sv);
                    data.SearchResult = Garage.Instance.SearchAllParkedVehiclesOnPrice(sv, true);
                    break;
                case '3':
                    data.SearchResult = Garage.Instance.SearchByRegNum(data.SearchValue);
                    break;
                case '4':
                    data.SearchResult = Garage.Instance.SearchAllParkedVehiclesOnVehicleType(data.VehicleTypeId);
                    break;
                case '5':
                    data.SearchResult = Garage.Instance.SearchAllParkingPlaces();
                    break;
            }
            data.Sort();
            return data.SearchResult;
        }

        public ActionResult Search(SearchData data)
        {
            ViewBag.VehicleTypes = Garage.Instance.GetVehicleTypes();

            data.SearchResult = _Search(data);
            return View(data);
        }
    }
}