using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGRental.Controllers
{
    
    public class AccountController : Controller
    {
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CurrentLease()
        {
            return View();
        }

        public IActionResult RenewLease()
        {
            return View();
        }

        public IActionResult EndLease()
        {
            return View();
        }
    }
}