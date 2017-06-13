using System;
using System.Collections.Generic;
using MATJParking.Web.Repositories;

namespace MATJParking.Web.Models
{
    public class SearchData
    {
        public string DropDown { get; set; }
        public string SearchValue { get; set; }
        public double SearchValDouble 
        { 
            get 
            {
                double result;
                double.TryParse(SearchValue, out result);
                return result;
            } 
        }
        public IEnumerable<ParkingPlace> SearchResult { get; set; }
        public int VehicleTypeId 
        {
            get 
            {
                if (DropDown.Length > 0 && DropDown[0] == '4')
                {
                    int result;
                    int.TryParse(DropDown.Substring(2), out result);
                    return result;
                }
                return 0;
            }
        }
        public string SearchPath 
        {
            get 
            {
                string result = "";

                if (VehicleTypeId > 0)
                {//DropDown 4.x
                    return "Vehicle type/" + Garage.Instance.GetVehicleType(VehicleTypeId).Name;
                }

                switch (DropDown)
                {
                    case "1": 
                        return "All parked vehicles";
                    case "2": 
                        result = "Price greater than/" + SearchValDouble + "SEK";
                        break;
                    case "3":
                        result = "Registration number/" + SearchValue.ToUpper() + "*";
                        break;
                    case "5":
                        result = "All parking places";
                        break;
                }
                return result;
            }
        }
        public char MenuOption
        {
            get
            {
                char menuOption = ' ';
                if (DropDown != null && DropDown.Length > 0)
                    menuOption = DropDown[0];
                return menuOption;
            }
        }

        public SearchData()
        {
            DropDown = "1"; //Default: all parked vehicles
            SearchValue = "";
            SearchResult = new ParkingPlace[0]; 
        }

    }
}