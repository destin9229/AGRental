using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGRental.Models
{
    //[Table("Properties")]
    public class Properties
    {
        [Key]
        public int Property_ID { get; set; }
        public string property_name { get; set; }
        public string address { get; set; }
    }
}
