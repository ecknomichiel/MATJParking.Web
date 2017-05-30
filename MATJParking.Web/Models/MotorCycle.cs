using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MATJParking
{
    class MotorCycle : Vehicle
    {
        protected override double GetPrice()
        {
            return StandardPrice;
        }
        protected override VehicleType GetVehicleType()
        {
            return VehicleType.Motorcycle;
        }
    }
}
