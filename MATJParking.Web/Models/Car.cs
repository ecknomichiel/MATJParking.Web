using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATJParking
{
    class Car : Vehicle
    {
        protected override double GetPrice()
        {
            var PriceForCar = StandardPrice * 2;
            return PriceForCar;
        }
        protected override VehicleType GetVehicleType()
        {
            return VehicleType.Car;
        }
    }
}
