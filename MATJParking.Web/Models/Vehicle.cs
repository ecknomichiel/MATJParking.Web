using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MATJParking.Web.Models
{
    public class Vehicle
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RegNumber { get; set; }

        public DateTime CheckInTime { get; set; }
        public VehicleType VehicleType { get; set; }
        public Owner Owner { get; set; }
      
        public double Price 
        {
            get { return GetPrice(); } 
        }
        public double ParkingTime
        {
            get 
            {
                DateTime endTime =  DateTime.Now;
                return endTime.Subtract(CheckInTime).TotalHours ;
            }
        }
        #endregion

        #region Private Methods
        private double GetPrice()
        {
            return Math.Round(ParkingTime * 15.0, 2) * VehicleType.PricingFactor;
        }
        #endregion
        #region Public Methods

        public override string ToString()
        {
            return String.Format("Registration number: {0}\n Vehicle type: {1}\n Checked in {2}" +
                "\n Current parking time {4} hours\n Current price: SEK {3}", RegNumber, VehicleType, CheckInTime, Price, Math.Round(ParkingTime, 2));
        }

        public void Assign(Vehicle source)
        {
            if (source != null)
            {
                CheckInTime = source.CheckInTime;
                VehicleType.Assign(source.VehicleType);
                Owner.Assign(source.Owner);
            }
        }
        
        public Vehicle()
        {
            VehicleType = new VehicleType();
            Owner = new Owner();
        }
        #endregion
       
    }
}
