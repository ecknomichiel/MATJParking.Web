using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MATJParking.Web.Models
{
    public class Owner
    {
        [Key]
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        // en ny Comments // en ny Comments // en ny Comments
        
        
        // en ny Comments
    }
}