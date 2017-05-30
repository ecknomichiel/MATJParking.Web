using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATJParking
{
    class Garage
    {
        private List<ParkingPlace> parkingPlaces = new List<ParkingPlace>();
        #region Private Methods
        private Vehicle CreateVehicle(VehicleType aVehicleType)
        {
            switch (aVehicleType)
            {
                case VehicleType.Motorcycle:
                    return new MotorCycle();
                case VehicleType.Car:
                    return new Car();
                case VehicleType.Bus:
                    return new Bus();
                case VehicleType.Truck:
                    return new Truck();
                default:
                    throw new EUnknownVehicleType(aVehicleType.ToString());
            }
        }
        private void LoadParkingPlaces()
        {
            int i;
            for (i = 0; i < 1; i++ ) 
            {
                parkingPlaces.Add(new ParkingPlace() {ID = "B" + i, VehicleType = VehicleType.Bus});
            }
            for (i = 0; i < 5; i++)
            {
                parkingPlaces.Add(new ParkingPlace() { ID = "T" + i, VehicleType = VehicleType.Truck });
            }
            for (i = 0; i < 50; i++)
            {
                parkingPlaces.Add(new ParkingPlace() { ID = "C" + i, VehicleType = VehicleType.Car });
            }
            for (i = 0; i < 20; i++)
            {
                parkingPlaces.Add(new ParkingPlace() { ID = "M" + i, VehicleType = VehicleType.Motorcycle });
            }
        }
        #endregion
        #region Public Methods
        public string CheckIn(string RegistrationNumber, VehicleType aVehicleType)
        {
            Vehicle vehicle = CreateVehicle(aVehicleType);
            vehicle.RegNumber = RegistrationNumber;
            ParkingPlace place = SearchPlaceWhereVehicleIsParked(RegistrationNumber);
            if (place != null)
                throw new EVehicleAlreadyCheckedIn(RegistrationNumber, place.ID);
            try
            { //If there is no available space for this type of car, an exception is raised (sequence contains no elements)
                place = parkingPlaces.Where(pl => pl.VehicleType == vehicle.VehicleType)
                                                .Where(pl => !pl.Occupied)
                                                .First();
            }
            catch (Exception)
            {// Throw our own exception with a custom message text
                throw new ENoPlaceForVehicle(aVehicleType.ToString());
            }
            place.Park(vehicle);

            return place.ID;
        }

        public void CheckOut(string RegistrationNumber)
        {
            ParkingPlace place = SearchPlaceWhereVehicleIsParked(RegistrationNumber);
            if (place == null)
                throw new EVehicleNotFound(RegistrationNumber);
            place.Unpark();
        }
        #endregion
        #region Search
        public IEnumerable<ParkingPlace> SearchAllParkingPlaces()
        {
            return parkingPlaces;
        }
        public IEnumerable<ParkingPlace> SearchAllParkedVehicles()
        {
            return parkingPlaces.Where(pl => pl.Occupied);
        }
        public IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnPrice(double aPrice, bool greaterThan)
        {
            if (greaterThan)
                return parkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.Price >= aPrice);
            else
                return parkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.Price <= aPrice);
        }

        public IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnParkingTime(double hours, bool greaterThan)
        {
            if (greaterThan)
                return parkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.ParkingTime >= hours);
            else
                return parkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.ParkingTime <= hours);
        }
        public Vehicle SearchVehicle(string aRegistrationNumber)
        {
            ParkingPlace park = SearchPlaceWhereVehicleIsParked(aRegistrationNumber);
            if (park == null)
            {
                return null;
            }
            else
            {
                return park.Vehicle;
            }
        }
        public ParkingPlace SearchPlaceWhereVehicleIsParked(string aRegistrationNumber)
        {// Can throw an exception if a programmer bypassed the checkin function to park a car
            return parkingPlaces.SingleOrDefault(pl => pl.Occupied && pl.VehicleRegNumber == aRegistrationNumber);
        }
        #endregion
        #region Constructor
        public Garage()
        {
            LoadParkingPlaces();
        }
        #endregion


        
    }
    #region Exceptions
    class EUnknownVehicleType: Exception
    {
        public EUnknownVehicleType(string vehicleType): base(String.Format("Unknows vehicle type: {0}", vehicleType))
        {
        }
    }

    class ENoPlaceForVehicle: Exception
    {
        public ENoPlaceForVehicle(string vehicleType): 
            base (String.Format("No place available for a {0}", vehicleType.ToString().ToLower()))
        {
        }
    }

    class EVehicleNotFound : Exception
    {
        public EVehicleNotFound(string aRegistrationNumber) :
            base(String.Format("Vehicle with registration number '{0}' not found.", aRegistrationNumber))
        {
        }
    }

    class EVehicleAlreadyCheckedIn : Exception
    {
        public EVehicleAlreadyCheckedIn(string aRegistrationNumber, string aParkingID) :
            base(String.Format("Vehicle with registration number '{0}' is already checked in at place {1}.", aRegistrationNumber, aParkingID))
        {
        }
    }
#endregion
}
