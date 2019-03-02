using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AGRental.Data;
using AGRental.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AGRental.ViewModels;

namespace AGRental.Controllers
{
    public class RentController : Controller
    {
        private AGRentalDBContext context;

        public RentController(AGRentalDBContext dbContext)
        {
            context = dbContext;
        }

        //Index
        //GET: /<controller>
        //Generates available properties from the Properties table
        public IActionResult Index()
        {
            //Verfies if an "user" is logged in
            //if (HttpContext.Session.GetString("Type") == "user")
            //{

            //Creates an object for the UserPropertiesViewModel
            UserPropertiesViewModel userPropertiesView = new UserPropertiesViewModel();

            //Creates an object for the list of Properties
            IList<Properties> properties = new List<Properties>();

            //Defines the hasProperty to equal false
            bool hasProperty=false;

            //Gets the current UserID for the session
            int UserID = HttpContext.Session.GetInt32("UserID") ?? 0;

            //Checks if the current User logged in is the UserProperties table
            UserProperties current_userProperty = context.UserProperties.SingleOrDefault(c => c.UserID == UserID);

            //Checks if the current UserProperties is null or not null
            if(current_userProperty != null)
            {
                hasProperty = true;
            }

            //Gets all the properties not taken in the Properties table
            properties = context.Properties.Where(c => c.is_taken == false).ToList();

            //Sets the property list in the view to properties
            userPropertiesView.propertyList = properties;

            //Sets has property in the view to hasProperty
            userPropertiesView.hasProperty = hasProperty;

            //Returns the userPropertiesView
            return View(userPropertiesView);
            //}


            //Redirects to login page if no user is logged in
            //else
            //{
                //ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
            //    return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            //}
        }

         //Gets the selected property that the User selects
         public IActionResult ViewProperty(int propertyId)
         {
                //Verfies if an "user" is logged in
              //  if (HttpContext.Session.GetString("Type") == "user")
              //  {

                    Properties selectedProperty = context.Properties.Single(c => c.Property_ID == propertyId);
                    return View(selectedProperty);
              //  }


                //Redirects to login page if no user is logged in
                //else
              //  {
                    //ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
              //      return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
              //  }
         }



        //Adds the property to the account
         public IActionResult addViewProperty(int Property_ID)
        {
            int UserID = HttpContext.Session.GetInt32("UserID") ?? 0;
            UserProperties new_property = new UserProperties();
            new_property.UserID = UserID;
            new_property.PropertyID = Property_ID;
            context.Add(new_property);
            context.SaveChanges();

            Properties selectedProperty = context.Properties.Single(c => c.Property_ID == Property_ID);
            selectedProperty.is_taken = true;
            context.SaveChanges();


            // Properties addPropertyView = new Properties(context.Properties.ToList());
            // return (addPropertyView);
            return View();
        }
        /*
                public IActionResult Add(addPropertyView addPropertyView)
                {
                    if (ModelState.IsValid)
                    {
                        Properties takeProperty = context.Properties.Single(c => c.Property_ID == addPropertyView.Property_ID);

                        // Add the new event to my existing events
                        Properties newEvent = new Properties
                        {
                            property_name = addPropertyView.property_name,
                            address = addPropertyView.address,
                            city = addPropertyView.city,
                            price = addPropertyView.price,
                        };

                        context.Properties.Add(newEvent);
                        context.SaveChanges();
                        return Redirect("/Rent");
                    }

                    addPropertyView.SetCategories(context.Properties.ToList());
                    return View(addPropertyView);
                }
        */

    }
}