using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MATJParking.Web.Models;

namespace MATJParking.Web.DataAccess
{
    public class GarageContextCreator: IGarageContextCreator
    {

        public string FactoryKey
        {
            get { return "database"; }
        }

        public IGarageContext CreateContext()
        {
            return new GarageContext();
        }
    }

    class GarageContext: DbContext, IGarageContext
    {
        private List<VehicleType> vehicleTypes = new List<VehicleType>();
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ParkingPlace> ParkingPlaces { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public Vehicle GetVehicleByID(string RegNumber)
        {
            return Vehicles.Include(v => v.Owner)
                            .Include(v => v.VehicleType)
                            .SingleOrDefault(v => v.RegNumber.ToUpper() == RegNumber.ToUpper());
        }
        public GarageContext() : base("DefaultConnection") 
        {
            vehicleTypes.AddRange(VehicleTypes);
        }


        public void AddVehicle(Vehicle vehicle)
        {
            SaveOwner(vehicle.Owner);
            if (vehicle != null && vehicle.VehicleType != null)
            {
                vehicle.VehicleType = GetVehicleTypeByID(vehicle.VehicleType.ID); //Make sure the vehicletype from the db is used
                Vehicles.Add(vehicle);
            }     
        }

        private void SaveOwner(Owner owner)
        {
            Owner target = Owners.Find(owner.Id);
            if (target == null)
                Owners.Add(owner);
            else
                target.Assign(owner);
        }

        public VehicleType GetVehicleTypeByID(int vehicleTypeId)
        {
            return vehicleTypes.Single(vt => vt.ID == vehicleTypeId);
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            Vehicle target = GetVehicleByID(vehicle.RegNumber);
            vehicle.VehicleType = GetVehicleTypeByID(vehicle.VehicleType.ID);
            target.Assign(vehicle);
        }

        public IEnumerable<ParkingPlace> GetAllParkingPlaces()
        {
            return ParkingPlaces
                .Include(p => p.Vehicle)
                                .Include(p => p.Vehicle.VehicleType)
                                .Include(p => p.Vehicle.Owner);
        }

        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            return vehicleTypes;
        }
    }


}