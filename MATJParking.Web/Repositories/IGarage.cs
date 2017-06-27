using System;
using System.Collections.Generic;
using MATJParking.Web.Models;

namespace MATJParking.Web.Repositories
{
    public interface IGarage
    {
        ParkingPlace CheckIn(CheckInData data);
        void CheckOut(string RegistrationNumber);
        IEnumerable<ParkingPlace> SearchAllParkingPlaces();
        IEnumerable<ParkingPlace> SearchAllParkedVehicles();
        IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnPrice(double aPrice, bool greaterThan);

        IEnumerable<OverviewLine> GetOverview();

        IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnParkingTime(double hours, bool greaterThan);
        Vehicle SearchVehicle(string aRegistrationNumber);
        VehicleType GetVehicleType(int aVehicleTypeId);
        ParkingPlace SearchPlaceWhereVehicleIsParked(string aRegistrationNumber);
        IEnumerable<VehicleType> GetVehicleTypes();

        IEnumerable<ParkingPlace> SearchByRegNum(string aRegistrationNumber);
        IEnumerable<ParkingPlace> SearchAllParkedVehiclesOnVehicleType(int vehicleTypeId);
    }
}
