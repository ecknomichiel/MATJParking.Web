using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MATJParking.Web.Models;
using System.Web.Mvc;

namespace MATJParking.Web.ModelBinder
{
    public class CheckinDataModelBinder: IModelBinder
    {
       

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //if (bindingContext.ModelName == typeof(CheckInData).FullName)
            {
                HttpRequestBase request = controllerContext.HttpContext.Request;
                int vehicleTypeId;
                int.TryParse(request.Form.Get("VehicleTypeId"), out vehicleTypeId);
                if (vehicleTypeId == 0)
                {
                    int.TryParse(request.Form.Get("Vehicle.VehicleType.Id"), out vehicleTypeId);
                }
                CheckInState st;
                Enum.TryParse<CheckInState>(request.Form.Get("State"), out st);               

                CheckInData result = new CheckInData
                {
                    RegistrationNumber = request.Form.Get("RegistrationNumber"),
                    VehicleTypeId = vehicleTypeId,
                    State = (CheckInState)st
                };
                Vehicle vehicle = new Vehicle();
                vehicle.RegNumber = result.RegistrationNumber;
                vehicle.VehicleType.ID = result.VehicleTypeId;
                vehicle.VehicleType.Name = request.Form.Get("VehicleType.Name");
                result.Vehicle = vehicle;

                Owner owner = new Owner();
                int customerId;
                int.TryParse(request.Form.Get("CustomerID"), out customerId);
                owner.Id = customerId;
                owner.FirstName = request.Form.Get("FirstName");
                owner.LastName = request.Form.Get("LastName");
                owner.PersonNumber = request.Form.Get("Personnumber");

                result.Owner = owner;
                vehicle.Owner = owner;
                return result;
            }
        }
    }
}