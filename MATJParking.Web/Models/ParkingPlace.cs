using System;
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
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public bool Occupied { get {return vehicle != null;} }
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
        public Vehicle Vehicle 
        { 
            get { return vehicle; }
            set { vehicle = value; } //Use only in case of loading from database!!!!
        }
        public VehicleType VehicleType { get; set; }
        #endregion
        #region Methods
        public void Park(Vehicle aVehicle)
        {
            vehicle = aVehicle;
            vehicle.CheckInTime = DateTime.Now;
        }
        public void Unpark()
        {
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
        #endregion

        public double ParkingTime 
        { 
            get
            {
                if (Occupied)
                {
                    return Vehicle.ParkingTime;
                }
                else
                {
                    return 0;
                }
            }
        }

        public double Price 
        { 
            get
            {
                if (Occupied)
                {
                    return Vehicle.Price;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

}
