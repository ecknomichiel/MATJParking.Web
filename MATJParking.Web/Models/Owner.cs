using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MATJParking.Web.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNumber { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

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
    }
}