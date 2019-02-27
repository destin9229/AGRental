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
            IList<Properties> properties = new List<Properties>();

            //Verfies if an "user" is logged in
            //if (HttpContext.Session.GetString("Type") == "user")
            //{
                properties = context.Properties.Where(c => c.is_taken == false).ToList();
                return View(properties);
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


        /*
        //Adds the c
         public IActionResult addProperty()
        {
            Properties addPropertyView = new Properties(context.Properties.ToList());
            return (addPropertyView);
        }

        public IActionResult Add(addPropertyView addPropertyView)
        {
            if (ModelState.IsValid)
            {
                Properties takeProperty = context.Properties.Single(c => c.ID == addPropertyView.Propert_ID);

                // Add the new event to my existing events
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    Category = newEventCategory,
                    Price = addEventViewModel.Price,
                    Date = addEventViewModel.Date + addEventViewModel.Time,
                    Created = DateTime.Now
                };

                context.Events.Add(newEvent);
                context.SaveChanges();


                // Success!!! event added...  return custom message
                TempData["Message"] = "Event " + newEvent.ID + " was successfully created.";
                TestFunctions.PrintConsoleMessage("SUCCESS, EVENT ADDED / CREATED");

                return Redirect("/Event");
            }

            addEventViewModel.SetCategories(context.Categories.ToList());
            return View(addEventViewModel);
        }

    */

    }
}