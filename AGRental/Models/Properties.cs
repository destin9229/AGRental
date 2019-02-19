using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AGRental.Models
{
    public class Properties
    {
        [Key]
        public int propertyID { get; set; }
        public string propertyName { get; set; }
        public string address { get; set; }
    }
}
