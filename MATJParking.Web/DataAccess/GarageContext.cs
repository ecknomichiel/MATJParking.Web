using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MATJParking.Web.Models;

namespace MATJParking.Web.DataAccess
{
    public class GarageContext: DbContext
    {
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingPlace> ParkingPlaces { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public Vehicle GetVehicleByID(string RegNumber)
        {
            return Vehicles.SingleOrDefault(v => v.RegNumber == RegNumber);//.Include(v => v.Owner);
        }
        public GarageContext() : base("DefaultConnection") { }

    }


}