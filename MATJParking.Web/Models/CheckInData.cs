using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MATJParking.Web.Models
{
    public class CheckInData
    { //This is a helper class for storing state and should not be stored in the database
        [Required]
        public string RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public Vehicle Vehicle { get; set; }
        public Owner Owner { get; set; }
        public ParkingPlace Place { get; set; }
        public IEnumerable<VehicleType> VehicleTypes { get; set; }
        public CheckInState State { get; set; }

        public CheckInData()
        {
            Vehicle = new Vehicle();
            Owner = new Owner();
            Place = new ParkingPlace();
            State = CheckInState.Initial;
        }
    }

    public enum CheckInState
    {
        Initial,
        SearchDone,
        NoPlaceForVehicle,
        AlreadyParked,
        Parked
    }
}