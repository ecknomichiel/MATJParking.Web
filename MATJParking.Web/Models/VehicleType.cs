using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MATJParking.Web.Models
{
    public class VehicleType
    {
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