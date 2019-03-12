<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
>>>>>>> Project Working with Images


namespace AGRental.Controllers
{
    public class AGRentalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}