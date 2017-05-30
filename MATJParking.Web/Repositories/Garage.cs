﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MATJParking.Web.Models;
using MATJParking.Web.DataAccess;

namespace MATJParking.Web.Repositories
{
    class Garage
    {
        private GarageContext context = new GarageContext();
        #region Private Methods
        private Vehicle CreateVehicle(VehicleType aVehicleType)
        {
            return new Vehicle() { VehicleType = aVehicleType };
        }
        private IEnumerable<ParkingPlace> ParkingPlaces { get { return context.ParkingPlaces; } }
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
                place = ParkingPlaces.Where(pl => pl.VehicleType == vehicle.VehicleType)
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

        public IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnParkingTime(double hours, bool greaterThan)
        {
            if (greaterThan)
                return ParkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.ParkingTime >= hours);
            else
                return ParkingPlaces.Where(pl => pl.Occupied && pl.Vehicle.ParkingTime <= hours);
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
            return ParkingPlaces.SingleOrDefault(pl => pl.Occupied && pl.VehicleRegNumber == aRegistrationNumber);
        }
        #endregion
        #region Constructor
        public Garage()
        {
            context = new GarageContext();
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