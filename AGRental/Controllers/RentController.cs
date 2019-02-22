using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AGRental.Data;
using AGRental.Models;
using Microsoft.EntityFrameworkCore;

namespace AGRental.Controllers
{
    // [Authorize]
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

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["property_name"] = String.IsNullOrEmpty(sortOrder) ? "propety_name_desc" : "propety_name";
            ViewData["address"] = sortOrder == "Address" ? "address=desc" : "Address";
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
            }
            return View(await Properties.AsNoTracking().ToListAsync());

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