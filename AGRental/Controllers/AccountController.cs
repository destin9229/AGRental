using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AGRental.Data;
using AGRental.Helpers;
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
            //if (HttpContext.Session.GetString("Type") == "user")
            //{
                return View();
           /* }

            //Returns error message that a user is not logged in
            else
            {
                ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }*/
        }


        //Current Lease
        public IActionResult CurrentLease(int User_ID)
        {
            bool userHasProperty = true;
            UserProperties current_user_properties = new UserProperties();
            Properties current_property = new Properties();
            Properties test_property = new Properties();
            int current_property_id = 0;

            if (ModelState.IsValid)
            {
                try
                {
                    current_user_properties = context.UserProperties.Single(c => c.UserID == User_ID);
                }
                catch
                {
                    userHasProperty = false;
                }
                if (userHasProperty)
                {
                    test_property = context.Properties.Single(c => c.Property_ID == 8);
                    test_property.price = 500;
                    context.SaveChanges();
                    current_property_id = current_user_properties.PropertyID;
                    current_property = context.Properties.Single(c => c.Property_ID == current_property_id);
                    return View(current_property);

                }
                else
                {
                    //ModelState.AddModelError("ServerError", "Your account does not have a property");
                    return RedirectToAction("ErrorPage", "Account", new { username = HttpContext.Session.GetString("user") });

                }

            }

            //Verfies if an "user" is logged in
            //if (HttpContext.Session.GetString("Type") == "user")
            //{

            /*}

            //Returns error message that a user is not logged in
            else
            {
                ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }*/
            return View();
        }

        public IActionResult ErrorPage()
        {
            return View();
        }

        //RENEW LEASE
        public IActionResult RenewLease()
        {
            //Verfies if an "user" is logged in
            //if (HttpContext.Session.GetString("Type") == "user")
            //{
                return View();
            /*}

            //Returns error message that a user is not logged in
            else
            {
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }*/
        }


        //END LEASE
        public IActionResult EndLease(int User_ID)
        {
            //Verfies if an "user" is logged in
            //if (HttpContext.Session.GetString("Type") == "user")
            //{

            //Gets the UserID for the sessions
            //int UserID = HttpContext.Session.GetInt32("UserID") ?? 0;

            UserProperties grabProperty = context.UserProperties.Single(c => c.UserID == User_ID);

            //Removes the item from the table
            context.Remove(context.UserProperties.Single(c=>c.UserID == User_ID));

            //Saves the new table
            context.SaveChanges();


            int Current_PropertyID = grabProperty.PropertyID;

            Properties updateProperty = context.Properties.Single(c => c.Property_ID == Current_PropertyID);

            updateProperty.is_taken = false;

            //Saves the new table
            context.SaveChanges();

                return View();
            /*}

            //Returns error message that a user is not logged in
            else
            {
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }*/
        }
    }
}