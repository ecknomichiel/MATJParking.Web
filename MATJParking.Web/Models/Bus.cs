using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATJParking
{
    class Bus : Vehicle
    {
        protected override double GetPrice()
        {
            var PriceForBus = StandardPrice * 4;
            return PriceForBus;
        }
        protected override VehicleType GetVehicleType()
        {
            return VehicleType.Bus;
        }
    }
}
