using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AGRental.Controllers
{
    public class logoutController : Controller
    {
        public IActionResult Index()
        {
            //FormsAuthentication.SignOut[];
            return View();
        }
    }
}