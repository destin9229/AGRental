using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AGRental.Models
{
    //Creates the values for the UserProperties table
    public class UserProperties
    {
        public int UserID { get; set; }
        public int PropertyID { get; set; }
    }
}
