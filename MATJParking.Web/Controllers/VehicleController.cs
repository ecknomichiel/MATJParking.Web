using MATJParking.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MATJParking.Web.Models;
using MATJParking.Web.Repositories;
using MATJParking.Web.ModelBinder;
using Newtonsoft.Json;


namespace MATJParking.Web.Controllers
{
    public class VehicleController : Controller
    {
        private IGarage garage;
        public VehicleController()
        {
            garage = Garage.Instance;
        }

        public VehicleController(IGarage garage = null) 
        {
            if (garage == null)
            {
                this.garage = Garage.Instance;
            }
            else
            {
                this.garage = garage;
            }
        }
    
        // GET: Vehicle
        public ActionResult Index(string id="")
        {
            ViewBag.Message = id;
            return View(garage.GetVehicleTypes());
        }
        [HttpPost]
        public ActionResult CheckIn([ModelBinder(typeof(CheckinDataModelBinder))] CheckInData checkInData)
        {

            switch (checkInData.State)
            {
                case CheckInState.Initial:
                    {//First time the Checkin is called: search for the car and show any details we might have
                        checkInData.Vehicle.Assign(garage.SearchVehicle(checkInData.RegistrationNumber));
                        checkInData.State = CheckInState.SearchDone;
                        ParkingPlace pl = garage.SearchPlaceWhereVehicleIsParked(checkInData.RegistrationNumber);
                        if (pl != null)
                        {
                            checkInData.Place = pl;
                            checkInData.State = CheckInState.AlreadyParked;
                        }
                        checkInData.VehicleTypes = garage.GetVehicleTypes();
                        return View(checkInData);
                    }
                case CheckInState.SearchDone:
                    {
                        //Check if data is ready for checkin
                        try
                        {
                            checkInData.Place = garage.CheckIn(checkInData);
                            checkInData.State = CheckInState.Parked;
                        }
                        catch (Exception e)
                        {
                            ViewBag.Message = e.Message;
                            if (e.GetType() == typeof(ENoPlaceForVehicle))
                            {
                                checkInData.State = CheckInState.NoPlaceForVehicle;
                                //Load miniml data for displying error page: Vehicle type
                                checkInData.Vehicle.VehicleType = garage.GetVehicleType(checkInData.VehicleTypeId);
                            }
                            if (e.GetType() == typeof(EVehicleAlreadyCheckedIn))
                            {
                                checkInData.State = CheckInState.AlreadyParked;
                                //Load data for displying error page: Parking place
                                checkInData.Place = garage.SearchPlaceWhereVehicleIsParked(checkInData.Vehicle.RegNumber);
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
            ParkingPlace pl = garage.SearchPlaceWhereVehicleIsParked(RegistrationNumber);
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
                garage.CheckOut(VehicleRegNumber);
            }
            catch (EVehicleNotFound e)
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
                pl = garage.SearchPlaceWhereVehicleIsParked(registrationNumber);
            }

            if (pl == null)
            {
                ViewBag.Message = "Cannot find car with registrationnumber " + registrationNumber;
            }
            return View(pl);
        }

        public ActionResult Overview()
        {
            return View(garage.GetOverview());
        }

        public string _getVehicleTypes()
        {
            return JsonConvert.SerializeObject(garage.GetVehicleTypes());
        }

        public string _Search(SearchData data)
        {
            if (data == null)
            {
                data = new SearchData();
            }

            switch (data.MenuOption)
            {
                case '1':
                    data.SearchResult = garage.SearchAllParkedVehicles();
                    break;
                case '2':
                    double sv;
                    double.TryParse(data.SearchValue, out sv);
                    data.SearchResult = garage.SearchAllParkedVehiclesOnPrice(sv, true);
                    break;
                case '3':
                    if (data.SearchValue == null || data.SearchValue == "")
                    {
                        data.SearchResult = garage.SearchAllParkedVehicles();
                    }
                    else
                    {
                        data.SearchResult = garage.SearchByRegNum(data.SearchValue);
                    }
                    
                    break;
                case '4':
                    data.SearchResult = garage.SearchAllParkedVehiclesOnVehicleType(data.VehicleTypeId);
                    break;
                case '5':
                    data.SearchResult = garage.SearchAllParkingPlaces();
                    break;
            }
            data.Sort();
            return data.JSONData;
        }

        public ActionResult Search(SearchData data)
        {
            ViewBag.VehicleTypes = garage.GetVehicleTypes();

            _Search(data);//Fills and sorts data

            return View(data);
        }
    }
}