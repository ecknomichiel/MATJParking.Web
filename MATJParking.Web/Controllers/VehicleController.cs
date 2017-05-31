using MATJParking.Web.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MATJParking.Web.Models;
using MATJParking.Web.Repositories;
using MATJParking.Web.Models;

namespace MATJParking.Web.Controllers
{
    public class VehicleController : Controller
    {
                
         GarageContext context = null;

        public VehicleController()
         {
           
         }
    
        // GET: Vehicle
        public ActionResult Index()
        {
            return View(Garage.Instance.GetVehicleTypes());
        }
    }

}