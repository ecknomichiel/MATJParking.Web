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

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            Owner other = obj as Owner;
            return this.Id == other.Id
                && this.FirstName == other.FirstName
                && this.PersonNumber == other.PersonNumber;
        }



        public void AssignUpdateabeData(Owner source)
        {
            if (source != null)
            {
                FirstName = source.FirstName;
                LastName = source.LastName;
                PersonNumber = source.PersonNumber;
            }
        }

        public Owner()
        {
            Id = 0;
            FirstName = "";
            LastName = "";
            PersonNumber = "";
            Vehicles = null;
        }
    }
}