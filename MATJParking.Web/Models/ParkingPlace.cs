﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATJParking.Web.Models
{
    public class ParkingPlace
    {
        private Vehicle vehicle;
        public VehicleType VehicleType { get; set; }
        [ForeignKey("VehicleType")]
        public int VehicleTypeID 
        { 
            get 
            {
                if (vehicle != null)
                    return VehicleType.ID;
                else
                    return 0;
            } 
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public bool Occupied { get {return vehicle != null;} }
        [ForeignKey("Vehicle")]
        public string VehicleRegNumber 
        { 
            get 
            {
                if (vehicle == null)
                    return "";
                else
                    return vehicle.RegNumber;
            } 
        }
        public Vehicle Vehicle { get { return vehicle; } }


        public void Park(Vehicle aVehicle)
        {
            vehicle = aVehicle;
            vehicle.CheckInTime = DateTime.Now;
        }
        public void Unpark()
        {
            vehicle.CheckOutTime = DateTime.Now;
            vehicle = null;
        }
        public override string ToString()
        {
            if (Occupied)
                return String.Format("{0} parking place {1}, occupied by '{2}'. Parking time: {4} hours Current price: SEK {3}", 
                        VehicleType, ID, vehicle.RegNumber, Math.Round(vehicle.Price, 2), Math.Round(vehicle.ParkingTime, 2));
            else
                return String.Format("{0} parking place {1}, empty", VehicleType, ID);
        }
    }

}
