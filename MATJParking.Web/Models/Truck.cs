using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATJParking
{
    class Truck : Vehicle
    {
        protected override double GetPrice()
        {
            var PriceForTruck = StandardPrice * 3;
            return PriceForTruck;
        }
        protected override VehicleType GetVehicleType()
        {
            return VehicleType.Truck;
        }

    }
}
