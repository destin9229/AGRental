using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace AGRental.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            //NEEDS TO BE IMPLEMENTED

            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }
            //Redirects user to login page
            else
            {
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }
    }
}