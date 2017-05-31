using System;

namespace MATJParking.Web.Models
{
    public class CheckInState
    { //This is a helper class for storing state and should not be stored in the database
        public string RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Owner Owner { get; set; }
        public ParkingPlace Place { get; set; }
        public bool ReadyToCheckin
        {
            get { return Place != null; }
        }

    }
}