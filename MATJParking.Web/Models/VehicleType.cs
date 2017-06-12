using System;
using System.ComponentModel.DataAnnotations;

namespace MATJParking.Web.Models
{
    public class VehicleType
    { //VehicleTpe is neary sttic data
        [Key]
        public int ID { get; set; }
        public string Name { get; set; } //Bus, Motorcycle, etc.
        public double PricingFactor { get; set; }

        public void Assign(VehicleType source)
        {
            ID = source.ID;
            Name = source.Name;
            PricingFactor = source.PricingFactor;
        }
    }
}