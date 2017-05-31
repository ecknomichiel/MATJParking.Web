using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MATJParking.Web.Models
{
    public class Vehicle
    {
        
        protected double StandardPrice
        { 
            get 
            { 
                return Math.Round(ParkingTime * 15.0, 2);
            }
        }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        [ForeignKey("VehicleType")]
        public int VehicleTypeID { get { return VehicleType.ID; } }
        public VehicleType VehicleType { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RegNumber { get; set; }
        public double Price 
        {
            get { return GetPrice(); } 
        }
        public double ParkingTime
        {
            get 
            {
                DateTime endTime = CheckOutTime;
                if (CheckOutTime < CheckInTime)
                {
                    endTime = DateTime.Now;
                }
                return endTime.Subtract(CheckInTime).TotalHours ;
            }
        }

        //methods

        public double GetPrice()
        {
            return StandardPrice * VehicleType.PricingFactor;
        }
        public override string ToString()
        {
            return String.Format("Registration number: {0}\n Vehicle type: {1}\n Checked in {2}" +
                "\n Current parking time {4} hours\n Current price: SEK {3}", RegNumber, VehicleType, CheckInTime, Price, Math.Round(ParkingTime, 2));
        }


       
    }
}
