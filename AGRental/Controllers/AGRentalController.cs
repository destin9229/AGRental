using Microsoft.AspNetCore.Mvc;


namespace AGRental.Controllers
{
    public class AGRentalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}