using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AGRental.Models
{
    public class UserProperties
    {
        public int UserID { get; set; }
        public int PropertyID { get; set; }
    }
}
