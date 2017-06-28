using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MATJParking.Web.Models;
using MATJParking.Web.DataAccess;

namespace MATJParking.Web.Repositories
{
    class Garage: IGarage
    {
        private IGarageContext context;

        private static Garage instance;

        #region Singleton pattern
        public static Garage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Garage();
                }
                return instance;
            }
        }
        #endregion
        #region Private Methods
        private IEnumerable<ParkingPlace> ParkingPlaces { get { return context.GetAllParkingPlaces(); } }
        private Vehicle CreateVehicle(VehicleType aVehicleType)
        {
            return new Vehicle() { VehicleType = aVehicleType };
        }
        public VehicleType GetVehicleType(int aVehicleTypeId)
        {
            return context.GetVehicleTypeByID(aVehicleTypeId);
        }
        public Vehicle CreateVehicle(int VehicleTypeId)
        {
            return CreateVehicle(GetVehicleType(VehicleTypeId));
        }
        #endregion
        #region Public Methods
        public ParkingPlace CheckIn(CheckInData data)
        {
            //Check if vehicle is already parked
            string uRegNr = data.RegistrationNumber.ToUpper();
            ParkingPlace place = SearchPlaceWhereVehicleIsParked(uRegNr);
            if (place != null)
                throw new EVehicleAlreadyCheckedIn(uRegNr, place.ID);

            //Lookup and store vehicle data
            Vehicle vehicle;
            vehicle = SearchVehicle(uRegNr);
            if (vehicle == null)
            {
                VehicleType vt = GetVehicleType(data.VehicleTypeId);
                vehicle = CreateVehicle(vt);
                vehicle.RegNumber = uRegNr;
            }
            vehicle.Owner.AssignUpdateabeData(data.Owner);
            
            
           // try
            { //If there is no available space for this type of car, an exception is raised (sequence contains no elements)
                place = AvailableParkingPlaceForVehicleType(vehicle.VehicleType).FirstOrDefault();
            }
           // catch (Exception)
            if (place == null)
            {// Throw our own exception with a custom message text
                throw new ENoPlaceForVehicle(vehicle.VehicleType.Name);
            }
            place.Park(vehicle);
            context.SaveChanges();

            return place;
        }

        private IEnumerable<ParkingPlace> AvailableParkingPlaceForVehicleType(VehicleType vehicleType)
        {
            return ParkingPlaces.Where(pl => pl.VehicleType == vehicleType )
                                            .Where(pl => !pl.Occupied);
        }

        //CheckOut
        public void CheckOut(string RegistrationNumber)
        {
            ParkingPlace place = SearchPlaceWhereVehicleIsParked(RegistrationNumber);
            if (place == null)
                throw new EVehicleNotFound(RegistrationNumber);
            place.Unpark();
            context.SaveChanges();
        }
        #endregion
        #region Search
        public IEnumerable<ParkingPlace> SearchAllParkingPlaces()
        {
            return ParkingPlaces;
        }
        public IEnumerable<ParkingPlace> SearchAllParkedVehicles()
        {
            return ParkingPlaces.Where(pl => pl.Occupied);
        }
        public IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnPrice(double aPrice, bool greaterThan)
        {
            if (greaterThan)
                return ParkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.Price >= aPrice);
            else
                return ParkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.Price <= aPrice);
        }

        public IEnumerable<OverviewLine> GetOverview()
        {
            List<VehicleType> types = new List<VehicleType>();
            types.AddRange(GetVehicleTypes());
            foreach (VehicleType vt in types)
            {
                yield return new OverviewLine() { VehicleType = vt, NumAvailablePlaces = AvailableParkingPlaceForVehicleType(vt).Count() };
            }
        }

        public IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnParkingTime(double hours, bool greaterThan)
        {
            if (greaterThan)
                return ParkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.ParkingTime >= hours);
            else
                return ParkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.ParkingTime <= hours);
        }
        public Vehicle SearchVehicle(string aRegistrationNumber)
        {
            return context.GetVehicleByID(aRegistrationNumber);
        }
        public ParkingPlace SearchPlaceWhereVehicleIsParked(string aRegistrationNumber)
        {// Can throw an exception if a programmer bypassed the checkin function to park a vehicle and parked a vehicle twice
            return ParkingPlaces.SingleOrDefault(pl => pl.Occupied && pl.VehicleRegNumber.ToUpper() == aRegistrationNumber.ToUpper());
        }
        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return context.GetVehicleTypes();
        }

        public IEnumerable<ParkingPlace> SearchByRegNum(string aRegistrationNumber)
        {
            return ParkingPlaces.Where(pl => pl.Occupied && pl.VehicleRegNumber.StartsWith(aRegistrationNumber.ToUpper()));
        }
        public IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnVehicleType(int vehicleTypeId)
        {
            return ParkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.VehicleType.ID == vehicleTypeId);
        }
        
        
        #endregion
        #region Constructor
        private Garage()
        {
            context = new GarageContext();
        }
        #endregion




        
    }
    #region Exceptions
    public class EUnknownVehicleType: Exception
    {
        public EUnknownVehicleType(string vehicleType): base(String.Format("Unknows vehicle type: {0}", vehicleType))
        {
        }
    }

    public class ENoPlaceForVehicle: Exception
    {
        public ENoPlaceForVehicle(string vehicleType): 
            base (String.Format("No place available for a {0}", vehicleType.ToString().ToLower()))
        {
        }
    }

    public class EVehicleNotFound : Exception
    {
        public EVehicleNotFound(string aRegistrationNumber) :
            base(String.Format("Vehicle with registration number '{0}' not found.", aRegistrationNumber))
        {
        }
    }

    public class EVehicleAlreadyCheckedIn : Exception
    {
        public EVehicleAlreadyCheckedIn(string aRegistrationNumber, string aParkingID) :
            base(String.Format("Vehicle with registration number '{0}' is already checked in at place {1}.", aRegistrationNumber, aParkingID))
        {
        }
    }
#endregion
}
