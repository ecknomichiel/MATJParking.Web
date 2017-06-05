using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MATJParking.Web.Models
{
    public class OverviewLine
    {
        public VehicleType VehicleType { get; set; }
        public int NumAvailablePlaces { get; set; }
    }
}