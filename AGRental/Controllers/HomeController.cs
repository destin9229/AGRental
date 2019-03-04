using AGRental.Data;
using AGRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace VenueApp.Controllers
{
    public class HomeController : Controller
    {
        private AGRentalDBContext context;

        public HomeController(AGRentalDBContext dbContext)
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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
