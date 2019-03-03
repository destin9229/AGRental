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

        //INDEX
        //Generates available properties from the Properties table
        public IActionResult Index()
        {
                //Verfies if an "user" is logged in
                if (HttpContext.Session.GetString("Type") == "user")
                {
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
                }

                else
                {
                    //Redirects to login page if no user is logged in
                    return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
                }
        }

         //Gets the selected property that the User selects
         public IActionResult ViewProperty(int propertyId)
         {
                //Verfies if an "user" is logged in
                if (HttpContext.Session.GetString("Type") == "user")
                {
                    //Looks for Property_ID that user selected from the Properties table
                    Properties selectedProperty = context.Properties.Single(c => c.Property_ID == propertyId);
                    return View(selectedProperty);
                }
                else
                {
                    //Redirects to login page if no user is logged in
                    return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
                }
         }
        
        //Adds the property to the account
         public IActionResult addViewProperty(int Property_ID)
        {
            //Declares the current UserID session to UserID
            int UserID = HttpContext.Session.GetInt32("UserID") ?? 0;

            //Creates a new_property object for the UserProperties tables
            UserProperties new_property = new UserProperties();
            
            //Sets to UserID to new_property.UserID
            new_property.UserID = UserID;

            //Sets to Property_ID to new_property.PropertyID
            new_property.PropertyID = Property_ID;
            
            //Adds to new_property to the UserProperties table
            context.Add(new_property);
            
            //Saves the changes to the table
            context.SaveChanges();

            //Looks for the Property_ID in the Properties table
            Properties selectedProperty = context.Properties.Single(c => c.Property_ID == Property_ID);

            //Changes that Property_ID is_taken column to true
            selectedProperty.is_taken = true;

            //Saves the changes to the table
            context.SaveChanges();

            return View();
        }
    }
}