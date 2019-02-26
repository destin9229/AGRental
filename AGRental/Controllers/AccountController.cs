using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGRental.Data;
using AGRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AGRental.Controllers
{
    public class AccountController : Controller
    {
        private AGRentalDBContext context;

        public AccountController(AGRentalDBContext dbContext)
        {
            context = dbContext;
        }

        //INDEX     GET: /<controller>
        public IActionResult Index()
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }

            //Returns error message that a user is not logged in
            else
            {
                ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }


        //Current Lease
        public IActionResult CurrentLease()
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }

            //Returns error message that a user is not logged in
            else
            {
                ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }


        //RENEW LEASE
        public IActionResult RenewLease()
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }

            //Returns error message that a user is not logged in
            else
            {
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }


        //END LEASE
        public IActionResult EndLease()
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }

            //Returns error message that a user is not logged in
            else
            {
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }
    }
}