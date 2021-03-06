﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AGRental.Models
{

    //Creates the values for the Properties table
    public class Properties
    {
        [Key]
        public int Property_ID { get; set; }
        public string property_name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int price { get; set; }
        public int bedrooms { get; set; }
        public int bathrooms { get; set; }
        public bool is_taken { get; set; }
    }
}
