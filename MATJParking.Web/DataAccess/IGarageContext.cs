using System;
using System.Collections.Generic;
using MATJParking.Web.Models;

namespace MATJParking.Web.DataAccess
{
    public interface IGarageContext
    {
        Vehicle GetVehicleByID(string RegNumber);
        VehicleType GetVehicleTypeByID(int vehicleTypeId);
        IEnumerable<ParkingPlace> GetAllParkingPlaces();
        IEnumerable<VehicleType> GetVehicleTypes();
        int SaveChanges();
    }
}
