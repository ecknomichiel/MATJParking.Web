using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MATJParking.Web.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PersonNumber { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public string FullName 
        { 
            get 
            {
                string result = FirstName.Trim();
                if (!String.IsNullOrWhiteSpace(result))
                {
                    if (!String.IsNullOrWhiteSpace(LastName))
                    {
                        result += " " + LastName;
                    }
                    else
                    {
                        result = LastName;
                    }
                }
                return result;
            } 
        }

        public void Assign(Owner source)
        {
            if (source != null)
            {
                Id = source.Id;
                FirstName = source.FirstName;
                LastName = source.LastName;
                PersonNumber = source.PersonNumber;
                Vehicles = source.Vehicles;
            }
        }



        internal void AssignUpdateabeData(Owner source)
        {
            if (source != null)
            {
                FirstName = source.FirstName;
                LastName = source.LastName;
                PersonNumber = source.PersonNumber;
            }
        }
    }
}