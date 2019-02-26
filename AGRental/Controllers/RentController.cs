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
        /*
        public IActionResult Index()
        {
            return View(context.Properties.ToList());
        }
        */
       
        //Index
        //GET: /<controller>
        //Generates Search and Sort the available properties from the Properties table
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {

            //Verfies if an "user" is logged in
            if (HttpContext.Session.GetString("Type") == "user")
            {
                ViewData["property_name"] = String.IsNullOrEmpty(sortOrder) ? "propety_name_desc" : "propety_name";
                ViewData["address"] = sortOrder == "Address" ? "address=desc" : "Address";
                ViewData["city"] = sortOrder == "City" ? "city=desc" : "City";
                ViewData["price"] = sortOrder == "Price" ? "price=desc" : "Price";

                ViewData["CurrentFilter"] = searchString;
                var Properties = from p in context.Properties
                                 select p;
                if (!String.IsNullOrEmpty(searchString))
                {
                    Properties = Properties.Where(p => p.property_name.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "propety_name_desc":
                        Properties = Properties.OrderByDescending(s => s.property_name);
                        break;
                    case "Address":
                        Properties = Properties.OrderBy(s => s.address);
                        break;
                    case "city":
                        Properties = Properties.OrderBy(s => s.city);
                        break;
                    case "price":
                        Properties = Properties.OrderBy(s => s.price);
                        break;
                }
                return View(await Properties.AsNoTracking().ToListAsync());
            }

            //Redirects to login page if no user is logged in
            else
            {
                //ViewBag.ErrorMessage = "You must be logged into to gain access to this feature";
                return RedirectToAction("Login", "User", new { username = HttpContext.Session.GetString("user") });
            }
    

            /*
            public IActionResult ViewProperty(int Property_ID)
            {
                    List<Properties> properties = context
                        .Properties
                        .Include(property => property.property_name)
                        .Where(cm => cm.Property_ID == id)
                        .ToList();
                    RentPropertyViewModel viewModel = new RentPropertyViewModel
                    {
                        Property_name = property_name;
                        Address = address;
                    };
            }
            */

        }
    }
}