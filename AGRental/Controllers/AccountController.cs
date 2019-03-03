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

        //INDEX
        public IActionResult Index()
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }
            else
            {
                //Redirects to login page if no user is logged in
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }

        //CURRENT LEASE
        public IActionResult CurrentLease(int User_ID)
        {
             //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                    //Sets the userHasProperty equal to true
                    bool userHasProperty = true;
             
                    //Creates an object for the current_user_properties from the UserProperties table
                    UserProperties current_user_properties = new UserProperties();

                    //Creates an object for the current_property from the Properties table
                    Properties current_property = new Properties();

                    //Declares the current_property_id
                    int current_property_id = 0;

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            //Checks to see if the current UserID is in the UserProperties table
                            current_user_properties = context.UserProperties.Single(c => c.UserID == User_ID);
                        }
                        catch
                        {
                            //Sets the useerHasProperty to false
                            userHasProperty = false;
                        }
                        if (userHasProperty)
                        {
                            //Sets tbe current_user_properties ID to current_property_id
                            current_property_id = current_user_properties.PropertyID;

                            //Looks for the Property_ID in the Properties table
                            current_property = context.Properties.Single(c => c.Property_ID == current_property_id);

                            //Saves the changes to the table
                            context.SaveChanges();
                        
                            //Returns the current property to the View
                            return View(current_property);
                        }

                        else
                        {
                            //Redirects to Error page
                            return RedirectToAction("ErrorPage", "Account", new { username = HttpContext.Session.GetString("user") });
                        }
                    }
                    return View();
            }

            else
            {
                //Redirects to login page if no user is logged in
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }

        public IActionResult ErrorPage()
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }
            //Redirects to login page if no user is logged in
            else
            {
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }

        //END LEASE
        public IActionResult EndLease(int User_ID)
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                //Looks for the UserID in the UserProperties table
                UserProperties grabProperty = context.UserProperties.Single(c => c.UserID == User_ID);

                //Removes the item from the table
                context.Remove(context.UserProperties.Single(c => c.UserID == User_ID));

                //Saves the changes to the table
                context.SaveChanges();

                //Sets the PropertyID from the UserProperties and sets it to Current_PropertyID
                int Current_PropertyID = grabProperty.PropertyID;

                //Looks for the PropertyID that from the Properties table that matches the ID from the UserPropeties table
                Properties updateProperty = context.Properties.Single(c => c.Property_ID == Current_PropertyID);

                //Sets the is_taken column to false
                updateProperty.is_taken = false;

                //Saves the changes to the table
                context.SaveChanges();

                return View();
            }

            else
            {            
                //Redirects to login page if no user is logged in
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }

        // NEEDS TO BE IMPLEMENTED
        //RENEW LEASE
        public IActionResult RenewLease()
        {
            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                return View();
            }
            //Redirects to login page if no user is logged in
            else
            {
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
        }
    
    }
}