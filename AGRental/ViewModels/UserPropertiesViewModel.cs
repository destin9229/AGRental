﻿using AGRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGRental.ViewModels
{
    //Creates a UserPropertiesViewModel for the Login page
    public class UserPropertiesViewModel
    {
        public IList<Properties> propertyList { get; set; }
        public bool hasProperty { get; set; }
    }
}
